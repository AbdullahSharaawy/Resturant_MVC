using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Services;
using System.Threading.Tasks;

namespace Resturant_PL.Controllers
{
    public class BranchController : Controller
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        public async Task<IActionResult> Index()
        {
            return View("Branches", await _branchService.GetList());
        }

        public async Task<IActionResult> Update(int id)
        {
            return View("Update", await _branchService.GetById(id));
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        public async Task<IActionResult> SaveEdit(BranchDTO _UpdateBranch)
        {
            if (await _branchService.Update(_UpdateBranch) == null)
            {
                RedirectToAction("Update", _UpdateBranch);
                TempData["ErrorMessage"] = "Failed to update the record.";
            }
            else
            {
                TempData["SuccessMessage"] = "Record updated successfully!";
            }
            return View("Branches", await _branchService.GetList());
        }

        public async Task<IActionResult> SaveNew(BranchDTO _CreateBranch)
        {
            if (await _branchService.Create(_CreateBranch) == null)
            {
                RedirectToAction("Create", _CreateBranch);
                TempData["ErrorMessage"] = "Failed to create a new record.";
            }
            else
            {
                TempData["SuccessMessage"] = "New Record is created successfully!";
            }
            return View("Branches", await _branchService.GetList());
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _branchService.Delete(id))
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