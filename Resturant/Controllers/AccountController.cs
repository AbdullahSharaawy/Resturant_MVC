using Castle.Core.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels;
using Resturant_BLL.ImplementServices;
using Resturant_BLL.Services;
using Resturant_DAL.Entities;
using System.Configuration;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Resturant_PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly Resturant_BLL.Services.IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, Resturant_BLL.Services.IEmailSender emailSender, IConfiguration configuration)
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

        public IActionResult Login()
        {
            return View("Login");
        }

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> AccountSettings()
        {
            return PartialView("_AccountSettings");
        }
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
                if (appuser != null)
                {
                    bool found = await userManager.CheckPasswordAsync(appuser, loginDTO.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(appuser, loginDTO.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", "Email or password is incorrect.");
            }

            return View("Login", loginDTO);
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
        public async Task<IActionResult> AdminAccountSettings()
        {
            return View();
        }
    }
}