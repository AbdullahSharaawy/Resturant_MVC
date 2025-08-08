using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels.OrderDTOS;
using Resturant_BLL.DTOModels.OrderItemDTOs;
using Resturant_BLL.Mapperly;
using Resturant_BLL.Services;
using Resturant_DAL.Entities;
using System.Security.Claims;

namespace Resturant_PL.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;

        public OrderController(IOrderService orderService, IOrderItemService orderitemservice)
        {
            _orderService = orderService;
            _orderItemService = orderitemservice;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetList();
            return View(orders);
        }
        public async Task<IActionResult> ViewDraftOrder(int id)
        {
            var order = await _orderService.GetDraftOrderById(id);
            if (order == null)
                return NotFound();

            return View(order);
        }
        public async Task<IActionResult> Checkout(int id)
        {
            var order = await _orderService.GetDraftOrderById(id);
            if (order == null)
                return NotFound();

            var confirmedOrder = new ConfirmedOrderDTO
            {
                OrderID = order.OrderID,
                OrderItems = order.OrderItems,
                Address = ""
            };
            return View(confirmedOrder);
        }


        [HttpPost]
        public async Task<IActionResult> Checkout(ConfirmedOrderDTO order)
        {
            var result = await _orderService.ConfirmOrder(order);
            if (result == null)
                return BadRequest();

            return RedirectToAction("Details",  new { id = order.OrderID});
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetById(id);
            if (order == null)
                return NotFound();

            return View(order);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _orderService.Delete(id);
            if (!success)
                return NotFound();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var orders = await _orderService.GetOrdersByUserId(userId);
            return PartialView("~/Views/Shared/_MyOrders.cshtml", orders);
        }

    }
}
