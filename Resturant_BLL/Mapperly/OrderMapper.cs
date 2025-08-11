using Resturant_BLL.DTOModels.OrderDTOS;
using Resturant_DAL.Entities;
using Riok.Mapperly.Abstractions;

namespace Resturant_BLL.Mapperly
{
    [Mapper]
    public partial class OrderMapper
    {
        public partial AdminOrderDTO MapToAdminOrderDTO(Order order);
        public partial ReadOrderDTO MapToReadMyOrderDTO(Order order);
        public partial List<ReadOrderDTO> MapToMyOrderDTOList(List<Order> orders);
        public partial DraftOrderDTO MapToDraftOrderDTO(Order order);
        public partial ReadOrderDTO MapToReadtOrderDTO(Order order);
        public partial ConfirmedOrderDTO MapToConfirmedOrderDTO(Order order);
        public partial ShippedOrderDTO MapToShippedOrderDTO(Order order);
        public partial Order MapToOrder(ConfirmedOrderDTO orderDTO);
        public partial Order MapToOrder(DraftOrderDTO orderDTO);
        public partial ReadOrderDTO MapToReadtOrderDTO(DraftOrderDTO orderDTO);
        public partial Order MapToOrder(AdminOrderDTO orderDTO);
        public partial List<AdminOrderDTO> MapToOrderDTOList(List<Order> orders);
        public partial List<AdminOrderDTO> MapToAdminOrderDTOList(List<Order> orders);
    }
}
