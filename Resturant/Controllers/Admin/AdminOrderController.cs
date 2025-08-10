using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels;
using Resturant_BLL.DTOModels.OrderDTOS;
using Resturant_BLL.Services;
using Resturant_DAL.Entities;

namespace Resturant_PL.Controllers.Admin
{
    public class AdminOrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;

        public AdminOrderController(IOrderService orderService, IOrderItemService orderitemservice)
        {
            _orderService = orderService;
            _orderItemService = orderitemservice;
        }
        public async Task<IActionResult> Index()
        {
            return View("Orders", await _orderService.GetList());
        }

        public async Task<IActionResult> Update(int id)
        {
            return View("Update", await _orderService.GetById(id));
        }

        public async Task<IActionResult> SaveEdit(AdminOrderDTO _NewOrder)
        {

            if (!ModelState.IsValid)
            {

                _NewOrder.OrderItems = await _orderItemService.GetList();

                return View("Update", _NewOrder);
            }
            else
            {
                if (await _orderService.Update(_NewOrder) == null)
                {
                    return View("Update", _NewOrder);
                }

                else
                    TempData["SuccessMessage"] = "Record updated successfully!";
            }
            return View("Orders", await _orderService.GetList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new AdminOrderDTO();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveNew(AdminOrderDTO _NewOrder)
        {
            if (!ModelState.IsValid)
            {
                _NewOrder.OrderItems = await _orderItemService.GetList();
                return View("Create",_NewOrder);
            }
            var created = await _orderService.CreateOrderByAdmin(_NewOrder);
            if (created== null)
            {
                return View("Create", _NewOrder);
            }
            TempData["SuccessMessage"] = "Record Saved successfully!";
            return View("Orders", await _orderService.GetList());
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _orderService.Delete(id))
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
