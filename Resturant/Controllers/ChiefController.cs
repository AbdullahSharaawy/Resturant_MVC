using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Services;

namespace Resturant_PL.Controllers
{
    public class ChiefController : Controller
    {
        private readonly IChiefService _chiefService;
        
        public ChiefController(IChiefService chiefService)
        {
            _chiefService = chiefService;
        }

        public IActionResult Index()
        {
            return View("Chiefs",_chiefService.GetList());
        }
        public IActionResult Update(int id)
        {

            return View("Update", _chiefService.GetUpdateChiefInfo(id));
        }
        public IActionResult Create()
        {
            return View("Create", _chiefService.GetCreateChiefInfo());
        }
        public IActionResult SaveEdit(UpdateChiefDTO _UpdateTable)
        {

            if (_chiefService.Update(_UpdateTable.chiefDTO) == null)
            {
                RedirectToAction("Update", _UpdateTable);
                TempData["ErrorMessage"] = "Failed to update the record.";
            }
            else
            {
                TempData["SuccessMessage"] = "Record updated successfully!";

            }
            return View("Chiefs", _chiefService.GetList());
        }
        public IActionResult SaveNew(UpdateChiefDTO _CreateChief)
        {

            if (_chiefService.Create(_CreateChief.chiefDTO) == null)
            {
                RedirectToAction("Create", _CreateChief);

                TempData["ErrorMessage"] = "Failed to create a new record.";

            }
            else
            {
                TempData["SuccessMessage"] = "New Record is created successfully!";
            }
            return View("Chiefs", _chiefService.GetList());
        }
        public IActionResult Delete(int id)
        {
            if (_chiefService.Delete(id))
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
