using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels.OrderDTOs;
using Resturant_BLL.Services;

namespace Resturant_PL.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetList();
            return View(orders);
        }
        public async Task<IActionResult> ViewOrder(int id)
        {
            var order = await _orderService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order); 
        }
        public async Task<IActionResult> ConfirmOrder(int id)
        {
            var order = await _orderService.GetById(id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(ConfirmedOrderDTO order)
        {
            var result = await _orderService.ConfirmOrder(order);
            if (result == null)
                return BadRequest();

            return RedirectToAction("ViewOrder", new { id = order.OrderID});
        }

        public async Task<IActionResult> Delete(int id)
        {
            var success = await _orderService.Delete(id);
            if (!success)
                return NotFound();

            return RedirectToAction("Index");
        }

    }
}
