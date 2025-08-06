using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels.OrderItemDTOs;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;

namespace Resturant_BLL.Services
{
    public class OrderItemService : IOrderItemService
    {

        private readonly IRepository<OrderItem> _CR;
        private readonly IRepository<MenueItem> _MenuRepo;

        public OrderItemService(IRepository<OrderItem> rR, IRepository<MenueItem> _menuRepo)
        {
            _CR = rR;
            _MenuRepo = _menuRepo;

        }
        public async Task<List<ReadOrderItemDTO>> GetList()
        {
            var orderItems = await _CR.GetAll();
            if (orderItems == null || orderItems.Count == 0)
            {
                return new List<ReadOrderItemDTO>();
            }
            List<ReadOrderItemDTO> orderItemDTOs = new OrderItemMapper().MapToOrderItemDTOList(orderItems);
            return orderItemDTOs;
        }
        public async Task<ReadOrderItemDTO?> GetById(int id)
        {
            OrderItem orderItem = await _CR.GetByID(id);
            if (orderItem == null)
            {
                return null;
            }
            ReadOrderItemDTO orderItemDTO = new OrderItemMapper().MapToReadOrderItemDTO(orderItem);
            return orderItemDTO;
        }
        public async Task<UpdateOrderItemDTO?> GetToUpdateById(int id)
        {
            OrderItem orderItem = await _CR.GetByID(id);
            if (orderItem == null)
            {
                return null;
            }
            UpdateOrderItemDTO orderItemDTO = new OrderItemMapper().MapToUpdateOrderItemDTO(orderItem);
            return orderItemDTO;
        }
        public async Task<CreateOrderItemDTO?> Create(CreateOrderItemDTO orderitem)
        {
            if (orderitem == null)
            {
                return null;
            }

            var menuItem = await _MenuRepo.GetByID(orderitem.MenueItem.ItemID);
            if (menuItem == null)
            {
                return null;
            }

            OrderItem orderItem = new OrderItemMapper().MapToOrderItem(orderitem);
            orderItem.ItemID = orderitem.MenueItem.ItemID;
            orderItem.Price = menuItem.Price * orderitem.Quantity; ;
            orderItem.CreatedOn = DateTime.UtcNow;
            orderItem.CreatedBy = "Current User";
            orderItem.IsDeleted = false;

            await _CR.Create(orderItem);
            return orderitem;
        }
        public async Task<UpdateOrderItemDTO?> Update(UpdateOrderItemDTO orderitem)
        {
            if (orderitem == null)
            {
                return null;
            }

            var existingOrderItem = await _CR.GetByID(orderitem.OrderItemID);
            if (existingOrderItem == null)
                return null;

            var menuItem = await _MenuRepo.GetByID(orderitem.MenueItem.ItemID);
            if (menuItem == null)
                return null;

            existingOrderItem.Quantity = orderitem.Quantity;
            existingOrderItem.Price = menuItem.Price * orderitem.Quantity;
            existingOrderItem.ModifiedOn = DateTime.UtcNow;
            existingOrderItem.ModifiedBy = "Current User";
            existingOrderItem.IsDeleted = false;

            await _CR.Update(existingOrderItem);
            return orderitem;
        }
        public async Task<bool> Delete(int id)
        {
            OrderItem orderItem = await _CR.GetByID(id);
            if (orderItem == null || orderItem.IsDeleted == true)
            {
                return false;
            }
            orderItem.IsDeleted = true;
            orderItem.DeletedOn = DateTime.UtcNow;
            orderItem.DeletedBy = "Current User";
            await _CR.Update(orderItem);
            return true;
        }
    }
}
