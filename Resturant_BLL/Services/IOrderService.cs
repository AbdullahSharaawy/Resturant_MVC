using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;

namespace Resturant_BLL.Services
{
    public interface IOrderService
    {
        public List<OrderDTO> GetList();
        public OrderDTO? GetById(int id);
        public OrderDTO? Create(OrderDTO orderDTO);
        public OrderDTO? Update(OrderDTO orderDTO);
        public bool Delete(int id);
    }
}
