using Microsoft.AspNetCore.Mvc;

namespace Resturant_PL.Controllers
{
    public class ChiefController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
