using Resturant_BLL.DTOModels.OrderDTOS;
using Resturant_DAL.Entities;
using Riok.Mapperly.Abstractions;

namespace Resturant_BLL.Mapperly
{
    [Mapper]
    public partial class OrderMapper
    {
        public partial AdminOrderDTO MapToAdminOrderDTO(Orders order);
        public partial ReadOrderDTO MapToReadMyOrderDTO(Orders order);
        public partial List<ReadOrderDTO> MapToMyOrderDTOList(List<Orders> orders);
        public partial DraftOrderDTO MapToDraftOrderDTO(Orders order);
        public partial ReadOrderDTO MapToReadtOrderDTO(Orders order);
        public partial ConfirmedOrderDTO MapToConfirmedOrderDTO(Orders order);
        public partial ShippedOrderDTO MapToShippedOrderDTO(Orders order);
        public partial Orders MapToOrder(ConfirmedOrderDTO orderDTO);
        public partial Orders MapToOrder(DraftOrderDTO orderDTO);
        public partial ReadOrderDTO MapToReadtOrderDTO(DraftOrderDTO orderDTO);
        public partial Orders MapToOrder(AdminOrderDTO orderDTO);
        public partial List<AdminOrderDTO> MapToOrderDTOList(List<Orders> orders);
        public partial List<AdminOrderDTO> MapToAdminOrderDTOList(List<Orders> orders);
    }
}
