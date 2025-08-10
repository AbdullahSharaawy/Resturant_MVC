using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_BLL.DTOModels.OrderItemDTOs;
using Resturant_DAL.Entities;
using Riok.Mapperly.Abstractions;

namespace Resturant_BLL.Mapperly
{
    [Mapper]
    public partial class OrderItemMapper
    {
        [MapProperty(nameof(OrderItem.MenueItem), nameof(ReadOrderItemDTO.MenueItem))]
        public partial CreateOrderItemDTO MapToCreateOrderItemDTO(OrderItem orderItem);
        public partial UpdateOrderItemDTO MapToUpdateOrderItemDTO(OrderItem orderItem);
       
        public partial ReadOrderItemDTO MapToReadOrderItemDTO(OrderItem orderItem);
        public partial OrderItem MapToOrderItem(CreateOrderItemDTO orderItemDTO);
        public partial OrderItem MapToOrderItem(UpdateOrderItemDTO orderItemDTO);

        public partial List<ReadOrderItemDTO> MapToOrderItemDTOList(List<OrderItem> orderItems);

    }
}
