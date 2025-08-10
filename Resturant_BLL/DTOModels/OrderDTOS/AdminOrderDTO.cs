using Resturant_BLL.DTOModels.OrderItemDTOs;
using Resturant_DAL.Entities;

namespace Resturant_BLL.DTOModels.OrderDTOS
{
    public class AdminOrderDTO
    {
        public int OrderId { get; set; }
        public string UserID { get; set; }
        public List<ReadOrderItemDTO> OrderItems { get; set; }
        public decimal Weight { get; set; }
        public string Address { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal OrderCost { get; set; }
        public decimal ShipmentCost { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
