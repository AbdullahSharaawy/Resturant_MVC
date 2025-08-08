using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Resturant_BLL.DTOModels.OrderDTOS;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
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
        public async Task<List<AdminOrderDTO>> GetList()
        {
            List<Order> orders = new List<Order>();
            orders = await _CR.GetAll();

            List<AdminOrderDTO> orderDTOs = new List<AdminOrderDTO>();
            if (orders == null || orders.Count == 0)
            {
                return new List<AdminOrderDTO>();
            }
            orderDTOs = new OrderMapper().MapToOrderDTOList(orders);
            return orderDTOs;
        }
        public async Task<AdminOrderDTO?> GetById(int id)
        {
            Order order = await _CR.GetByID(id);
            if (order == null)
            {
                return null;
            }

            AdminOrderDTO orderDTO = new OrderMapper().MapToReadOrderDTO(order);
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
        public async Task<Order?> CreateOrderByAdmin(DTOModels.OrderDTOS.AdminOrderDTO Neworder)
        {

            var order = new OrderMapper().MapToOrder(Neworder);
            order.CreatedOn = DateTime.UtcNow;
            order.CreatedBy = "User";
            order.OrderStatus = OrderStatus.Draft;
            order.IsDeleted = false;
            await _CR.Create(order);
            return order;
        }
        public async Task<Order?> CreateDraftOrder(DraftOrderDTO Neworder)
        {

            var order = new OrderMapper().MapToOrder(Neworder);
                order.UserID = "UserId";
                order.CreatedOn = DateTime.UtcNow;
                order.CreatedBy = "User";
                order.OrderStatus = OrderStatus.Draft;
                order.IsDeleted = false;
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
        public async Task<AdminOrderDTO?> Update(AdminOrderDTO order)
        {
            if (order == null)
            {
                return null;
            }

            var modifiedOrder = await _CR.GetByID(order.OrderID);
            if (modifiedOrder == null)
                return null;
            modifiedOrder.OrderCost= order.OrderCost;
            modifiedOrder.ShipmentCost = order.ShipmentCost;
            modifiedOrder.Weight= order.Weight;
            modifiedOrder.Address= order.Address;
            modifiedOrder.OrderStatus= order.OrderStatus;
            modifiedOrder.ModifiedOn = DateTime.UtcNow;
            modifiedOrder.ModifiedBy = "Current User";
            modifiedOrder.IsDeleted = false;

            await _CR.Update(modifiedOrder);
            return order;
        }
        public async Task<List<AdminOrderDTO>> GetOrdersByUserId(string userId)
        {
            return await FilterBy(order => order.UserID == userId);
        }
        public async Task<List<AdminOrderDTO>?> FilterBy(Expression<Func<Order, bool>> filter)
        {
            List<Order> orders = await _CR.GetAllAsync(filter);
            if (orders == null || orders.Count == 0)
            {
                return null;
            }
            List<AdminOrderDTO> ordersDTO = new OrderMapper().MapToOrderDTOList(orders);
            return ordersDTO;
        }

        public Task<Order> CreateDraftOrder()
        {
            throw new NotImplementedException();
        }
    }
}
