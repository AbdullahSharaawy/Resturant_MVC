using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;

namespace Resturant_BLL.Services
{
    public interface IOrderItemService
    {
        public List<OrderItemDTO> GetList();
        public OrderItemDTO? GetById(int id);
        public OrderItemDTO? Create(OrderItemDTO orderItem);
        public OrderItemDTO? Update(OrderItemDTO orderItem);
        public bool Delete(int id);
    }
}
