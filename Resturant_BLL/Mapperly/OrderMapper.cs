using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;
using Riok.Mapperly.Abstractions;

namespace Resturant_BLL.Mapperly
{
    [Mapper]
    public partial class OrderMapper
    {
        public partial OrderDTO MapToOrderDTO(Order order);
        public partial Order MapToOrder(OrderDTO orderDTO);
        public partial List<OrderDTO> MapToOrderDTOList(List<Order> orders);
    }
}
