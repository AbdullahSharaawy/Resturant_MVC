using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;

namespace Resturant_BLL.Services
{
    public class OrderService:IOrderService
    {
        private readonly IRepository<Order> _CR;

        public OrderService(IRepository<Order> rR)
        {
            _CR = rR;
        }
        public List<OrderDTO> GetList()
        {
            List<Order> orders = new List<Order>();
            List<OrderDTO> orderDTOs = new List<OrderDTO>();
            orders = _CR.GetAll();
            if (orders == null || orders.Count == 0)
            {
                return null;
            }
            orderDTOs = new OrderMapper().MapToOrderDTOList(orders);
            return orderDTOs;
        }
        public OrderDTO? GetById(int id)
        {
            Order order = _CR.GetByID(id);
            if (order == null)
            {
                return null;
            }
            OrderDTO orderDTO = new OrderMapper().MapToOrderDTO(order);
            return orderDTO;
        }
        public OrderDTO? Create(OrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                return null;
            }
            Order order = new OrderMapper().MapToOrder(orderDTO);
            order.CreatedOn = DateTime.UtcNow;
            order.CreatedBy = "Current User";
            order.IsDeleted = false;
            _CR.Create(order);
            return orderDTO;
        }
        public OrderDTO? Update(OrderDTO orderDTO)
        {

            if (orderDTO == null)
            {
                return null;
            }
            Order order = new OrderMapper().MapToOrder(orderDTO);
            order.ModifiedOn = DateTime.UtcNow;
            order.ModifiedBy = "Current User";
            order.IsDeleted = false;
            _CR.Update(order);
            return orderDTO;
        }
        public bool Delete(int id)
        {
            Order orderItem = _CR.GetByID(id);
            if (orderItem == null || orderItem.IsDeleted == true)
            {
                return false;
            }
            orderItem.IsDeleted = true;
            orderItem.DeletedOn = DateTime.UtcNow;
            orderItem.DeletedBy = "Current User";
            _CR.Delete(orderItem);
            return true;
        }
    }
}
