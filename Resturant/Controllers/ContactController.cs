using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Services;

namespace Resturant_PL.Controllers
{
    public class ContactController : Controller
    {
        private readonly IBranchService _branchService;
        private readonly IEmailSenderService _ESS;
        private readonly string _email;
        private readonly string _password;
        private readonly string _host;
        public ContactController(IBranchService branchService, IConfiguration configuration, IEmailSenderService eSS)
        {
            _branchService = branchService;
            _email = configuration["Email:ResturantEmail"];
            _password = configuration["Email:ResturantEmailPassword"];
            _host = configuration["Email:ResturantEmailHost"];
            _ESS = eSS;
        }

        public async Task<IActionResult> Index()
        {
            ContactDTO contactDTO = new ContactDTO();
            contactDTO.branchDTOs = await _branchService.GetList();
            return View("ContactUs",contactDTO);
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
