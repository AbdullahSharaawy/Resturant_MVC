using Microsoft.AspNetCore.Mvc;

namespace Resturant_PL.Controllers
{
    public class Order : Controller
    {
        public IActionResult Index()
        {
            return View("MyOrder");
        }
        public IActionResult MyAddress() 
        {
            return View("MyAddress");
        }
    }
}
