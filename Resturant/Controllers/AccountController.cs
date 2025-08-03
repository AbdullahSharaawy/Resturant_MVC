using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;

namespace Resturant_PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
                User appuser = new User
                {
                    Email = registerDTO.Email,
                    UserName = registerDTO.Email,
                    FirstName=registerDTO.FirstName,
                    LastName=registerDTO.LastName,
                };

                IdentityResult result = await userManager.CreateAsync(appuser, registerDTO.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(appuser, false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View("Register", registerDTO);
        }
    }
}