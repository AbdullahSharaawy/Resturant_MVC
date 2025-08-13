using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels.ReviewDTOS;
using Resturant_BLL.Mapperly;
using Resturant_BLL.Services;
using Resturant_DAL.Entities;
using System.Threading.Tasks;

namespace Resturant_PL.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly UserManager<User> _usuManager;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        public async Task<IActionResult> BestReviews()
        {
            return PartialView("_BestReviews", await _reviewService.GetList(r => r.IsDeleted == false && r.Rate >= 4));
        }
        public async Task<IActionResult> Index()
        {

            return View("Reviews", await _reviewService.GetList(r=>r.IsDeleted==false ));
        }
        public async Task<IActionResult>  LeaveReview()
        {
            return PartialView("_LeaveReview");
        }
        [Authorize]
        public async Task<IActionResult> SaveReview(ReadReviewDTO readReview)
        {
            if (ModelState.IsValid)
            {
                if(await _reviewService.Create(readReview)==null)
                    return Json(new {success=false, message="save your review operation is failed."});
                return Json(new { success = true, message = "the operation is done successfuly but note that you can put only one review." });
            }
            return Json(new { success=false , message="Please enter your rate and description."});
        }
       
       
       

       

        public async Task<IActionResult> Delete(int id)
        {
            if (await _reviewService.Delete(id))
            {
                TempData["SuccessMessage"] = "Record deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete the record.";
            }
            return RedirectToAction("Index");
        }
    }
}
