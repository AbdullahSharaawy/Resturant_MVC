using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels.AccountDTOS;
using Resturant_BLL.DTOModels.IdentityDTOS;
using Resturant_BLL.ImplementServices;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Sharaawy_BL.Helper;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Resturant_PL.Controllers
{

    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
       
        private readonly SignInManager<User> signInManager;
        private readonly Resturant_BLL.Services.IEmailSenderService _emailSender;
        private readonly IConfiguration _configuration;
        public UserProfileController(UserManager<User> userManager, SignInManager<User> signInManager, Resturant_BLL.Services.IEmailSenderService emailSender, IConfiguration configuration)
        {
            _userManager = userManager;
            this.signInManager = signInManager;
            _emailSender = emailSender;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            UserProfileDTO userProfileDTO = new UserMapper().MapToUserProfileDTO(user);
            return View("UserProfile",userProfileDTO);
        }
        public async Task<IActionResult> UserProfileInfo()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }
            UserProfileDTO userProfileDTO = new UserProfileDTO();
            // Access the custom properties
            userProfileDTO.FirstName = user.FirstName;
            userProfileDTO.LastName = user.LastName;
            userProfileDTO.Email = user.Email;
            return PartialView("_ProfileInfo", userProfileDTO);
        }
         public async Task<IActionResult> SaveEditProfileInfo(UserProfileDTO userProfileDTO)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState.ContainsKey("Email"))
                {
                    // Get errors for the specific key
                    var errors = ModelState["Email"]?.Errors;

                    if (errors != null && errors.Count > 0)
                    {
                        return Json(new { success = false, message = "Please enter a valid Email." });

                    }
                }
                return Json(new { success = false, message = "Pleas fill all Form." });

            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
           
            // Generate email confirmation token
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "UserProfile",
                new { userId = user.Id, code = code ,FirstName=userProfileDTO.FirstName,LastName=userProfileDTO.LastName,Email=userProfileDTO.Email},
                protocol: Request.Scheme);
            EmailSettings emailSettings = new EmailSettings
            {
                SmtpHost = _configuration["EmailSettings:SmtpHost"],
                SmtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]),
                SmtpUseSSL = bool.Parse(_configuration["EmailSettings:SmtpUseSSL"]),
                SmtpUser = _configuration["EmailSettings:SmtpUser"],
                SmtpPassword = _configuration["EmailSettings:SmtpPassword"],
                FromName = _configuration["EmailSettings:FromName"]
            };
            await _emailSender.SendEmailAsync(
       user.Email,
       "Confirm your email",
       $@"
    <div style='font-family: Arial, sans-serif; background-color: #f9f9f9; padding: 20px;'>
        <div style='max-width: 600px; margin: auto; background-color: #ffffff; border-radius: 8px; 
                    box-shadow: 0 2px 8px rgba(0,0,0,0.1); padding: 30px;'>
            <h2 style='color: #cda45e; text-align: center;'>Confirm Your Email</h2>
            <p style='color: #333; font-size: 16px; line-height: 1.6;'>
                 Please confirm your account by clicking the button below.
            </p>
            <div style='text-align: center; margin: 30px 0;'>
                <a href='{HtmlEncoder.Default.Encode(callbackUrl)}' 
                   style='background-color: #cda45e; color: #fff; text-decoration: none; padding: 12px 25px;
                          border-radius: 5px; font-size: 16px; font-weight: bold; display: inline-block;'>
                    Confirm My Email
                </a>
            </div>
           
        </div>
    </div>
    ", emailSettings);

            return Json(new
            {
                success = true,
               
                redirectUrl = Url.Action("UpdateProfileConfirmation", "Account", new { email = userProfileDTO.Email })
            });


        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code,string FirstName, string LastName, string Email)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                return View("UserProfile");    
            }
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Email = Email;
            result=await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return View("UserProfile");
            }
            return View("ConfirmEmail");
        }
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Json(new { success = false, message = "failed to upload the image" });
            }
            string imagePath = Upload.UploadFile("UserImages", file);
            var user = await _userManager.GetUserAsync(User);
            user.ImagePath = imagePath;
            IdentityResult result=await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var existingClaims = await _userManager.GetClaimsAsync(user);
        
        // Find and remove the existing ImagePath claim if it exists
        var oldImageClaim = existingClaims.FirstOrDefault(c => c.Type == "ImagePath");
        if (oldImageClaim != null)
        {
            await _userManager.RemoveClaimAsync(user, oldImageClaim);
        }
        
        // Add the new claim
        var newClaim = new Claim("ImagePath", user.ImagePath ?? "PersonIcon.svg");
        await _userManager.AddClaimAsync(user, newClaim);
                
                //_userManager.ReplaceClaimAsync(user,)
                return Json(new { success = true, message = "upload the image is done successfuly" });
            }
            return Json(new { success = false,message="failed to upload the image" });
        }
        [HttpGet]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }


    }
}
