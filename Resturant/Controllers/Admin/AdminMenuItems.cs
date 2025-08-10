using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels;
using Resturant_BLL.DTOModels.OrderDTOS;
using Resturant_BLL.Services;
using Resturant_DAL.Entities;

namespace Resturant_PL.Controllers.Admin
{
    public class AdminMenuItemsController : Controller
    {
        private readonly IMenueItemService _MenuService;

        public AdminMenuItemsController(IMenueItemService menuservice)
        {
            _MenuService = menuservice;
        }
        public async Task<IActionResult> Index()
        {
            return View("MenuItems", await _MenuService.GetList());
        }

        public async Task<IActionResult> Update(int id)
        {
            return View("Update", await _MenuService.GetById(id));
        }

        public async Task<IActionResult> SaveEdit(MenueItemDTO menueItem)
        {

            if (!ModelState.IsValid)
            {

                return View("Update", menueItem);
            }
            if (await _MenuService.Update(menueItem) == null)
            {
                return View("Update", menueItem);
            }

            TempData["SuccessMessage"] = "Record updated successfully!";
            return View("Orders", await _MenuService.GetList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new MenueItemDTO();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveNew(MenueItemDTO menueItem)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", menueItem);
            }
            var created = await _MenuService.Create(menueItem);
            if (created == null)
            {
                return View("Create", menueItem);
            }
            TempData["SuccessMessage"] = "Record Saved successfully!";
            return View("MenuItems", await _MenuService.GetList());
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _MenuService.Delete(id))
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
