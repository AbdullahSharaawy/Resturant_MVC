using Microsoft.AspNetCore.Mvc;

namespace Resturant_PL.Controllers
{
    public class UserProfileController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View("UserProfile");
        }
        
    }
}
