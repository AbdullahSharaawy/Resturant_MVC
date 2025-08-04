using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Services;
using System.Threading.Tasks;

namespace Resturant_PL.Controllers
{
    public class ChiefController : Controller
    {
        private readonly IChiefService _chiefService;

        public ChiefController(IChiefService chiefService)
        {
            _chiefService = chiefService;
        }

        public async Task<IActionResult> Index()
        {
            return View("Chiefs", await _chiefService.GetList());
        }

        public async Task<IActionResult> Update(int id)
        {
            return View("Update", await _chiefService.GetUpdateChiefInfo(id));
        }

        public async Task<IActionResult> Create()
        {
            return View("Create", await _chiefService.GetCreateChiefInfo());
        }

        public async Task<IActionResult> SaveEdit(UpdateChiefDTO _UpdateTable)
        {
            if (await _chiefService.Update(_UpdateTable.chiefDTO) == null)
            {
                RedirectToAction("Update", _UpdateTable);
                TempData["ErrorMessage"] = "Failed to update the record.";
            }
            else
            {
                TempData["SuccessMessage"] = "Record updated successfully!";
            }
            return View("Chiefs", await _chiefService.GetList());
        }

        public async Task<IActionResult> SaveNew(UpdateChiefDTO _CreateChief)
        {
            if (await _chiefService.Create(_CreateChief.chiefDTO) == null)
            {
                RedirectToAction("Create", _CreateChief);
                TempData["ErrorMessage"] = "Failed to create a new record.";
            }
            else
            {
                TempData["SuccessMessage"] = "New Record is created successfully!";
            }
            return View("Chiefs", await _chiefService.GetList());
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _chiefService.Delete(id))
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