using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels.OrderItemDTOs;
using Resturant_DAL.Entities;

namespace Resturant_BLL.Services
{
    public interface IOrderItemService
    {
        public Task<List<ReadOrderItemDTO>> GetList();
        public Task<ReadOrderItemDTO?> GetById(int id);
        public Task<CreateOrderItemDTO?> Create(CreateOrderItemDTO orderItem);
        public Task<UpdateOrderItemDTO?> Update(UpdateOrderItemDTO orderItem);
        public Task<bool> Delete(int id);
    }
}
