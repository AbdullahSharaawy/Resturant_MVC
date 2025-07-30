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
    public partial class OrderItemMapper
    {
        public partial OrderItemDTO MapToOrderItemDTO(OrderItem orderItem);
        public partial OrderItem MapToOrderItem(OrderItemDTO orderItemDTO);
        public partial List<OrderItemDTO> MapToOrderItemDTOList(List<OrderItem> orderItems);

    }
}
