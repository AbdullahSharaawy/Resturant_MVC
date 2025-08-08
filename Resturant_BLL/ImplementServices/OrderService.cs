using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Resturant_BLL.DTOModels.OrderDTOs;
using Resturant_BLL.DTOModels.OrderDTOS;
using Resturant_BLL.DTOModels.OrderItemDTOs;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.ImplementRepository;
using Resturant_DAL.Repository;

namespace Resturant_BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _CR;
        private readonly UserManager<User> _user;

        public OrderService(IRepository<Order> rR, UserManager<User> user)
        {
            _CR = rR;
            _user = user;
        }
        public async Task<List<ReadOrderDTO>> GetList()
        {
            List<Order> orders = new List<Order>();
            orders = await _CR.GetAll();

            List<ReadOrderDTO> orderDTOs = new List<ReadOrderDTO>();
            if (orders == null || orders.Count == 0)
            {
                return new List<ReadOrderDTO>();
            }
            orderDTOs = new OrderMapper().MapToOrderDTOList(orders);
            return orderDTOs;
        }
        public async Task<ReadOrderDTO?> GetById(int id)
        {
            Order order = await _CR.GetByID(id);
            if (order == null)
            {
                return null;
            }

            ReadOrderDTO orderDTO = new OrderMapper().MapToReadOrderDTO(order);
            return orderDTO;
        }
        public async Task<DraftOrderDTO?> GetDraftOrderById(int id)
        {
            Order order = await _CR.GetByID(id);
            if (order == null)
            {
                return null;
            }

            DraftOrderDTO orderDTO = new OrderMapper().MapToDraftOrderDTO(order);
            return orderDTO;
        }
        public async Task<ShippedOrderDTO?> GetShippedOrder(int id)
        {
            Order order = await _CR.GetByID(id);
            if (order == null)
            {
                return null;
            }
            ShippedOrderDTO orderDTO = new OrderMapper().MapToShippedOrderDTO(order);
            return orderDTO;
        }
        public async Task<Order?> CreateDraftOrder()
        {

            var order = new Order
            {
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "User",
                OrderStatus = OrderStatus.Draft,
                IsDeleted = false
            };

            await _CR.Create(order);
            return order;
        }
        public async Task<ConfirmedOrderDTO?> ConfirmOrder(ConfirmedOrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                return null;
            }

            if (orderDTO.OrderItems == null || orderDTO.OrderItems.Count == 0)
            {
                return null;
            }

            Order confirmedorder = await _CR.GetByID(orderDTO.OrderID);

            if (confirmedorder == null)
            {
                return null;
            }

            decimal price = confirmedorder.OrderItems.Sum(item => item.Price);

            confirmedorder.OrderStatus = OrderStatus.Confirmed;
            confirmedorder.OrderCost = price;
            confirmedorder.ShipmentCost = 2000;
            confirmedorder.Address = orderDTO.Address;
            confirmedorder.TotalAmount = confirmedorder.OrderCost + confirmedorder.ShipmentCost;
            await _CR.Update(confirmedorder);
            return new OrderMapper().MapToConfirmedOrderDTO(confirmedorder); ;
        }
        public async Task<bool> Delete(int id)
        {
            Order order = await _CR.GetByID(id);
            if (order == null || order.IsDeleted == true)
            {
                return false;
            }
            order.IsDeleted = true;
            order.DeletedOn = DateTime.UtcNow;
            order.DeletedBy = "Current User";
            await _CR.Update(order);
            return true;
        }
        public async Task<List<ReadOrderDTO>> GetOrdersByUserId(string userId)
        {
            return await FilterBy(order => order.UserID == userId);
        }
        public async Task<List<ReadOrderDTO>?> FilterBy(Expression<Func<Order, bool>> filter)
        {
            List<Order> orders = await _CR.GetAllAsync(filter);
            if (orders == null || orders.Count == 0)
            {
                return null;
            }
            List<ReadOrderDTO> ordersDTO = new OrderMapper().MapToOrderDTOList(orders);
            return ordersDTO;
        }
    }
}
