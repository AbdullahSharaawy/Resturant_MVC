using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;

namespace Resturant_BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _CR;

        public OrderService(IRepository<Order> rR)
        {
            _CR = rR;
        }

        public async Task<List<OrderDTO>> GetList()
        {
            List<Order> orders = await _CR.GetAll();
            if (orders == null || orders.Count == 0)
            {
                return null;
            }
            List<OrderDTO> orderDTOs = new OrderMapper().MapToOrderDTOList(orders);
            return orderDTOs;
        }

        public async Task<OrderDTO?> GetById(int id)
        {
            Order order = await _CR.GetByID(id);
            if (order == null)
            {
                return null;
            }
            OrderDTO orderDTO = new OrderMapper().MapToOrderDTO(order);
            return orderDTO;
        }

        public async Task<OrderDTO?> Create(OrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                return null;
            }
            Order order = new OrderMapper().MapToOrder(orderDTO);
            order.CreatedOn = DateTime.UtcNow;
            order.CreatedBy = "Current User";
            order.IsDeleted = false;
            await _CR.Create(order);
            return orderDTO;
        }

        public async Task<OrderDTO?> Update(OrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                return null;
            }
            Order order = new OrderMapper().MapToOrder(orderDTO);
            order.ModifiedOn = DateTime.UtcNow;
            order.ModifiedBy = "Current User";
            order.IsDeleted = false;
            await _CR.Update(order);
            return orderDTO;
        }

        public async Task<bool> Delete(int id)
        {
            Order orderItem = await _CR.GetByID(id);
            if (orderItem == null || orderItem.IsDeleted == true)
            {
                return false;
            }
            orderItem.IsDeleted = true;
            orderItem.DeletedOn = DateTime.UtcNow;
            orderItem.DeletedBy = "Current User";
            await _CR.Delete(orderItem);
            return true;
        }
    }
}