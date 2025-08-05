using Resturant_BLL.DTOModels.OrderDTOs;
using Resturant_BLL.DTOModels.OrderDTOS;
using Resturant_DAL.Entities;
using Riok.Mapperly.Abstractions;

namespace Resturant_BLL.Mapperly
{
    [Mapper]
    public partial class OrderMapper
    {
        public partial ReadOrderDTO MapToReadOrderDTO(Order order);
        public partial DraftOrderDTO MapToDraftOrderDTO(Order order);
        public partial ConfirmedOrderDTO MapToConfirmedOrderDTO(Order order);
        public partial ShippedOrderDTO MapToShippedOrderDTO(Order order);
        public partial Order MapToOrder(ConfirmedOrderDTO orderDTO);
        public partial Order MapToOrder(DraftOrderDTO orderDTO);
        public partial List<ReadOrderDTO> MapToOrderDTOList(List<Order> orders);
    }
}
