using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.Services;

namespace Travel.PL.Controllers
{
    [Route("Payment")]
    public class PaymentController : Controller
    {
        private readonly IPaymobService _paymobService;

        public PaymentController(IPaymobService paymobService)
        {
            _paymobService = paymobService;
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(decimal amount)
        {
            var iframeUrl = await _paymobService.CreatePayment(amount);
            return Redirect(iframeUrl);
        }
             [HttpPost("Callback")]
public IActionResult Callback([FromBody] object response)
{
    // TODO: Verify response using HMAC
    return Ok();
}

    }
}
