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
            var existingItem = await _MenuService.GetById(id);
            if (existingItem == null)
                return NotFound();

            return View(existingItem);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEdit(MenueItemDTO menueItem)
        {

            if (!ModelState.IsValid)
            {

                return View("Update", menueItem);
            }
            var updatedItem = await _MenuService.Update(menueItem);
            if (updatedItem == null)
            {
                return View("Update", menueItem);
            }

            TempData["SuccessMessage"] = "Record updated successfully!";
            return RedirectToAction("Index");
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
            // return View("MenuItems", await _MenuService.GetList());
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
