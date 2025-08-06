using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Services;
using System.Threading.Tasks;

namespace Resturant_PL.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            return View("Reviews", await _reviewService.GetList());
        }

        public async Task<IActionResult> Update(int id)
        {
            return View("Update", await _reviewService.GetById(id));
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        public async Task<IActionResult> SaveEdit(ReviewDTO _UpdateReview)
        {
            if (await _reviewService.Update(_UpdateReview) == null)
            {
                RedirectToAction("Update", _UpdateReview);
                TempData["ErrorMessage"] = "Failed to update the record.";
            }
            else
            {
                TempData["SuccessMessage"] = "Record updated successfully!";
            }
            return View("Reviews", await _reviewService.GetList());
        }

        public async Task<IActionResult> SaveNew(ReviewDTO _CreateReview)
        {
            if (await _reviewService.Create(_CreateReview) == null)
            {
                RedirectToAction("Create", _CreateReview);
                TempData["ErrorMessage"] = "Failed to create a new record.";
            }
            else
            {
                TempData["SuccessMessage"] = "New record created successfully!";
            }
            return View("Reviews", await _reviewService.GetList());
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
