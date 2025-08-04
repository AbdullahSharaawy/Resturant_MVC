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
        //public async Task<IActionResult> SendEmail(string target_email,string subject,string message) 
        //{ 
        //   await _ESS.SendEmailAsync(_email, _password,target_email,subject,message,_host);
        //    return RedirectToAction("Index");
        //}

    }
}
