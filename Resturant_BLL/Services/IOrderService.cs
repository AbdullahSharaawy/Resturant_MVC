using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels.OrderDTOs;
using Resturant_BLL.DTOModels.OrderDTOS;
using Resturant_BLL.DTOModels.OrderItemDTOs;
using Resturant_DAL.Entities;

namespace Resturant_BLL.Services
{
    public interface IOrderService
    {
        public Task<List<ReadOrderDTO>> GetList();
        public Task<ReadOrderDTO?> GetById(int id);
        public Task<DraftOrderDTO?>? GetDraftOrderById(int id);
        public Task<ShippedOrderDTO?> GetShippedOrder(int id);
        public Task<Order> CreateDraftOrder();
        public Task<ConfirmedOrderDTO?> ConfirmOrder(ConfirmedOrderDTO orderDTO);
        public Task<bool> Delete(int id);
    }
}
