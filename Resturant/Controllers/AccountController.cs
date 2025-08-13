using Castle.Core.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Resturant_BLL.DTOModels;
using Resturant_BLL.DTOModels.AccountDTOS;
using Resturant_BLL.ImplementServices;
using Resturant_BLL.Services;
using Resturant_DAL.Entities;
using System.Configuration;
using System.Security.Claims;
using System.Text.Encodings.Web;
using static GenerativeAI.VertexAIModels;

namespace Resturant_PL.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly Resturant_BLL.Services.IEmailSenderService _emailSender;
        private readonly IConfiguration _configuration;
       

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, Resturant_BLL.Services.IEmailSenderService emailSender, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _emailSender = emailSender;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginDTO loginDTO = new LoginDTO
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View("Login",loginDTO);
        }
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var loginViewModel = new LoginDTO
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View("Login", loginViewModel);
            }

            var info = await signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");
                return View("Login", loginViewModel);
            }

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    var user = await userManager.FindByEmailAsync(email);
                    if(user==null)
                    {
                        user = new User
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };
                        await userManager.CreateAsync(user);
                    }

                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    await InsertImagePathToClaims(user);
                    return LocalRedirect(returnUrl);
                }
                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support Jon Pragim@PragimTech.com";

                return View("Error");
            }
        }
            [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        
        [Authorize]
        public async Task<IActionResult> AccountSettings()
        {
            return PartialView("_AccountSettings");
        }
        
        [Authorize]
        public async Task<IActionResult> UpdateAccountSettings(AccountSettingsDTO accountSettingsDTO)
        {
            if (accountSettingsDTO == null)
            {
                ModelState.AddModelError("NewSettings", "The New Settings is incorrect.");
                return Json(new { success = false, message = "Please enter The New Settings." });
            }
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (accountSettingsDTO.NewPassword == null)
            {
                ModelState.AddModelError("NewPassword", "The New Password is incorrect.");
                return Json(new { success = false, message = "Please enter The New Password." });

            }
            if (accountSettingsDTO.CurrentPassword == null)
            {
                ModelState.AddModelError("CurrentPassword", "The Current Password is incorrect.");
                return Json(new { success = false, message = "Please enter The Current Password." });

            }
            if (accountSettingsDTO.NewPassword == null)
            {
                ModelState.AddModelError("NewPassword", "The New Password is incorrect.");
                return Json(new { success = false, message = "Please enter The New Password." });

            }
            if (accountSettingsDTO.NewPassword != accountSettingsDTO.ConfirmNewPassword)
            {
                ModelState.AddModelError("ConfirmNewPassword", "The Confirm Password is incorrect.");
                return Json(new { success = false, message = "The Confirm Password is incorrect." });

            }
            if (await userManager.CheckPasswordAsync(user, accountSettingsDTO.CurrentPassword))
            {
                var result = await userManager.ChangePasswordAsync(user, accountSettingsDTO.CurrentPassword, accountSettingsDTO.NewPassword);
                if (result.Succeeded)
                {
                    return Json(new { success = true, message = "Update Account Settings is done Successfuly." });
                }
            }
            else
            {
                ModelState.AddModelError("CurrentPassword", "Current password is incorrect.");
                return Json(new { success = false, message = "Please enter The Valid Password." });

            }
            return Json(new { success = false, message = "Update Account Settings is Failed." });

        }
        [HttpPost]
        public async Task<IActionResult> SaveLogin(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                User appuser = await userManager.FindByEmailAsync(loginDTO.Email);
                if (appuser != null && appuser.EmailConfirmed)
                {
                    bool found = await userManager.CheckPasswordAsync(appuser, loginDTO.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(appuser, loginDTO.RememberMe);

                       await InsertImagePathToClaims(appuser);

                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", "Email or password is incorrect.");
            }

            return View("Login", loginDTO);
        }
        private async Task<bool> InsertImagePathToClaims(User appuser)
        {
            var existingClaims = await userManager.GetClaimsAsync(appuser);
            var imagePathClaims = existingClaims.Where(c => c.Type == "ImagePath");

            // Remove all existing ImagePath claims
            var removeResult = await userManager.RemoveClaimsAsync(appuser, imagePathClaims);

            var claims = new List<Claim>
                                    {
                                        new Claim("ImagePath", appuser.ImagePath ?? "PersonIcon.svg")
                                    };
           IdentityResult result= await userManager.AddClaimsAsync(appuser, claims);
            if (result.Succeeded)
                return true;
            return false;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = registerDTO.Email,
                    UserName = registerDTO.Email,
                    FirstName = registerDTO.FirstName,
                    LastName = registerDTO.LastName,
                };

                var result = await userManager.CreateAsync(user, registerDTO.Password);

                if (result.Succeeded)
                {
                    // Generate email confirmation token
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
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
                    // Send email
                   await  _emailSender.SendEmailAsync(
                        registerDTO.Email,
                        "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.",emailSettings);

                    // Don't sign in automatically - require email confirmation first
                    return RedirectToAction("RegisterConfirmation", new { email = registerDTO.Email });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View("Register", registerDTO);
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmResetPassword(string userId, string code)
            {
            ResetPasswordEditDTO resetPasswordEditDTO = new ResetPasswordEditDTO();
            resetPasswordEditDTO.token = code;
            resetPasswordEditDTO.userId = userId;
            return View("ResetPasswordEdit", resetPasswordEditDTO);
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("Index", "Home");
            }
           
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }
            
                
            return View("ConfirmEmail");
        }
        [HttpGet]
        public IActionResult RegisterConfirmation(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpGet]
        public IActionResult ResendEmailConfirmation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResendEmailConfirmation(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return View();
            }

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("RegisterConfirmation", new { email = email });
            }

            if (await userManager.IsEmailConfirmedAsync(user))
            {
                return RedirectToAction("Login");
            }

            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Account",
                new { userId = user.Id, code = code },
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
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.",emailSettings);

            return RedirectToAction("RegisterConfirmation", new { email = email });
        }
        [Authorize(Policy = "AdminOnly")]
        [Authorize]
        public async Task<IActionResult> AdminAccountSettings()
        {
            return View();
        }
        public async Task<IActionResult> ResetPassword()
        {
            return View();
        }
        public async Task<IActionResult> ConfirmResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            if (ModelState.IsValid) 
            {
                User user = await userManager.FindByEmailAsync(resetPasswordDTO.Email);
                if (user != null)
                {
                    var code = await userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmResetPassword",
                        "Account",
                        new { userId = user.Id, code = code },
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
                        resetPasswordDTO.Email,
                        "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.", emailSettings);

                    // Don't sign in automatically - require email confirmation first
                    return RedirectToAction("RegisterConfirmation", new { email = resetPasswordDTO.Email });

                }
                ModelState.AddModelError("", "Please Enter a valid Email");
            }
            return View("ResetPassword",resetPasswordDTO);
        }
      
        [Authorize]
        public async Task<IActionResult> NewPassword(ResetPasswordEditDTO resetPasswordEditDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(resetPasswordEditDTO.userId);
                var result = await userManager.ResetPasswordAsync(user, resetPasswordEditDTO.token, resetPasswordEditDTO.NewPassword);

                if (result.Succeeded)
                {
                    // Password was successfully updated.
                    // You can now redirect the user to a success page or the login page.
                    return RedirectToAction("Login");
                }
            }
            return View("ResetPasswordEdit",resetPasswordEditDTO);
        }
    }
}