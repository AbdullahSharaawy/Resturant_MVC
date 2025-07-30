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
    public class OrderItemService:IOrderItemService
    {

        private readonly IRepository<OrderItem> _CR;

        public OrderItemService(IRepository<OrderItem> rR)
        {
            _CR = rR;
        }
        public List<OrderItemDTO> GetList()
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            List<OrderItemDTO> orderItemDTOs = new List<OrderItemDTO>();
            orderItems = _CR.GetAll();
            if (orderItems == null || orderItems.Count == 0)
            {
                return null;
            }
            orderItemDTOs = new OrderItemMapper().MapToOrderItemDTOList(orderItems);
            return orderItemDTOs;
        }
        public OrderItemDTO? GetById(int id)
        {
            OrderItem orderItem = _CR.GetByID(id);
            if (orderItem == null) {
                return null;
            }
            OrderItemDTO orderItemDTO = new OrderItemMapper().MapToOrderItemDTO(orderItem);
            return orderItemDTO;
        }
        public OrderItemDTO? Create(OrderItemDTO orderitem)
        {
            if (orderitem == null)
            {
                return null;
            }
            OrderItem orderItem = new OrderItemMapper().MapToOrderItem(orderitem);
            orderItem.CreatedOn = DateTime.UtcNow;
            orderItem.CreatedBy = "Current User"; 
            orderItem.IsDeleted = false;
            _CR.Create(orderItem);
            return orderitem;
        }
        public OrderItemDTO? Update(OrderItemDTO orderitem)
        {
            if (orderitem == null)
            {
                return null;
            }
            OrderItem orderItem = new OrderItemMapper().MapToOrderItem(orderitem);
            orderItem.ModifiedOn = DateTime.UtcNow;
            orderItem.ModifiedBy = "Current User";
            orderItem.IsDeleted = false;
            _CR.Update(orderItem);
            return orderitem;
        }
        public bool Delete(int id)
        {
            OrderItem orderItem = _CR.GetByID(id);
            if (orderItem == null || orderItem.IsDeleted == true)
            {
                return false;
            }
            orderItem.IsDeleted = true;
            orderItem.DeletedOn = DateTime.UtcNow;
            orderItem.DeletedBy = "Current User";
            _CR.Delete(orderItem);
            return false;
        }
    }
}
