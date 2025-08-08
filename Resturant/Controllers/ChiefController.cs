using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Services;
using System.Threading.Tasks;

namespace Resturant_PL.Controllers
{
    public class ChiefController : Controller
    {
        private readonly IChiefService _CS;
        private readonly IBranchService _BS;

        public ChiefController(IChiefService chiefService, IBranchService bS)
        {
            _CS = chiefService;
            _BS = bS;
        }

        public async Task<IActionResult> Index()
        {
            return View("Chiefs", await _CS.GetList());
        }

        public async Task<IActionResult> Update(int id)
        {
            return View("Update", await _CS.GetUpdateChiefInfo(id));
        }

        public async Task<IActionResult> Create()
        {
            return View("Create", await _CS.GetCreateChiefInfo());
        }

        public async Task<IActionResult> SaveEdit(UpdateChiefDTO _UpdateChief)
        {

            if (!ModelState.IsValid)
            {

                _UpdateChief.Branches = await _BS.GetList();

                return View("Update", _UpdateChief);
            }
            else
            {
                if (await _CS.Update(_UpdateChief.chiefDTO) == null)
                {
                    return View("Update", _UpdateChief);
                }

                else
                    TempData["SuccessMessage"] = "Record updated successfully!";
            }
            return View("Chiefs", await _CS.GetList());
        }

        public async Task<IActionResult> SaveNew(UpdateChiefDTO _CreateChief)
        {
            if (!ModelState.IsValid)
            {

                _CreateChief.Branches = await _BS.GetList();

                return View("Update", _CreateChief);
            }
            else
            {
                if (await _CS.Update(_CreateChief.chiefDTO) == null)
                {
                    return View("Update", _CreateChief);
                }

                else
                    TempData["SuccessMessage"] = "Record updated successfully!";
            }
            return View("Chiefs", await _CS.GetList());
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _CS.Delete(id) )
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