using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels.AdminDTOS;
using Resturant_BLL.Services;
using Resturant_DAL.Entities;

namespace Resturant_PL.Controllers.Admin
{
    public class AdminDashboardController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IReservationService _RS;
        private readonly IReviewService _reviewS;

        public AdminDashboardController(UserManager<User> userManager, IReservationService rS, IReviewService reviewS)
        {
            _userManager = userManager;
            _RS = rS;
            _reviewS = reviewS;
        }

        public async Task<IActionResult> Index()
        {
            AdminDashBoardDTO adminDashBoardDTO = new AdminDashBoardDTO();
            adminDashBoardDTO.TotalUsers=_userManager.Users.Where(u=>u.EmailConfirmed).Count();
            var reservations =await _RS.GetList();
            var reviews = await _reviewS.GetList(r=>r.IsDeleted==false);
            adminDashBoardDTO.TotalReviews= reviews.Count();
            adminDashBoardDTO.TotalReservations= reservations.Where(r=>r.DateTime>=DateTime.UtcNow).Count();
            return View("Index",adminDashBoardDTO);
        }

    }
}
