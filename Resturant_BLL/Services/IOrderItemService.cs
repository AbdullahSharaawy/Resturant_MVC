using System.Collections.Generic;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;

namespace Resturant_BLL.Services
{
    public interface IOrderItemService
    {
        public Task<List<OrderItemDTO>> GetList();
        public Task<OrderItemDTO?> GetById(int id);
        public Task<OrderItemDTO?> Create(OrderItemDTO orderItem);
        public Task<OrderItemDTO?> Update(OrderItemDTO orderItem);
        public Task<bool> Delete(int id);
    }
}