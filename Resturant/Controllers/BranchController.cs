using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Services;

namespace Resturant_PL.Controllers
{
    public class BranchController : Controller
    { 
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }
       
        public IActionResult Index()
        {
            return View("Branches",_branchService.GetList());
        }
        public IActionResult Update(int id)
        {

            return View("Update", _branchService.GetById(id));
        }
        public IActionResult Create()
        {
            return View("Create");
        }
        public IActionResult SaveEdit(BranchDTO _UpdateBranch)
        {

            if (_branchService.Update(_UpdateBranch) == null)
            {
                RedirectToAction("Update", _UpdateBranch);
                TempData["ErrorMessage"] = "Failed to update the record.";
            }
            else
            {
                TempData["SuccessMessage"] = "Record updated successfully!";

            }
            return View("Branches", _branchService.GetList());
        }
        public IActionResult SaveNew(BranchDTO _CreateBranch)
        {

            if (_branchService.Create(_CreateBranch) == null)
            {
                RedirectToAction("Create", _CreateBranch);

                TempData["ErrorMessage"] = "Failed to create a new record.";

            }
            else
            {
                TempData["SuccessMessage"] = "New Record is created successfully!";
            }
            return View("Branches", _branchService.GetList());
        }
        public IActionResult Delete(int id)
        {
            if (_branchService.Delete(id))
            {
                TempData["SuccessMessage"] = "Record deleted successfully!";
            }
            // If deletion fails, you might want to show an error message
            else
            {
                TempData["ErrorMessage"] = "Failed to delete the record.";
            }
            return RedirectToAction("Index");
        }
    }
}
