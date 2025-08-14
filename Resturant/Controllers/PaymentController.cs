using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Resturant_BLL.Services;

namespace Resturant_PL.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _PS;

        public PaymentController(IPaymentService pS)
        {
            _PS = pS;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View("Index",await _PS.GetList());
        }
    }
}
