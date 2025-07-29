using Microsoft.AspNetCore.Mvc;

namespace Resturant_PL.Controllers
{
    public class ResturantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
