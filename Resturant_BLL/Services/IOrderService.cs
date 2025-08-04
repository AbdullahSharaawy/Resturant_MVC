using System.Collections.Generic;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;

namespace Resturant_BLL.Services
{
    public interface IOrderService
    {
        public Task<List<OrderDTO>> GetList();
        public Task<OrderDTO?> GetById(int id);
        public Task<OrderDTO?> Create(OrderDTO orderDTO);
        public Task<OrderDTO?> Update(OrderDTO orderDTO);
        public Task<bool> Delete(int id);
    }
}