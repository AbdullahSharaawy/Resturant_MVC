using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Services;
using System.Threading.Tasks;

namespace Resturant_PL.Controllers
{
  
    public class BranchController : Controller
    {
        private readonly IBranchService _BS;

        public BranchController(IBranchService branchService)
        {
            _BS = branchService;
        }

        public async Task<IActionResult> Index()
        {
            return View("Branches", await _BS.GetList());
        }

        public async Task<IActionResult> Update(int id)
        {
            return View("Update", await _BS.GetById(id));
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        public async Task<IActionResult> SaveEdit(BranchDTO _UpdateBranch)
        {

            if (!ModelState.IsValid)
            {

                return View("Update", _UpdateBranch);
            }
            else
            {
                if (await _BS.Update(_UpdateBranch) == null)
                {
                    return View("Update", _UpdateBranch);
                }

                else
                    TempData["SuccessMessage"] = "Record updated successfully!";
            }
            return View("Branches", await _BS.GetList());
        }

        public async Task<IActionResult> SaveNew(BranchDTO _CreateBranch)
        {

            if (!ModelState.IsValid)
            {

                return View("Update", _CreateBranch);
            }
            else
            {
                if (await _BS.Create(_CreateBranch) == null)
                {
                    return View("Update", _CreateBranch);
                }

                else
                    TempData["SuccessMessage"] = "Record updated successfully!";
            }
            return View("Branches", await _BS.GetList());
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _BS.Delete(id) )
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