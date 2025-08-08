using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels.OrderItemDTOs;
using Resturant_DAL.Entities;

namespace Resturant_BLL.DTOModels.OrderDTOS
{
    public class ShippedOrderDTO
    {
        public int OrderID { get; set; }
        public List<ReadOrderItemDTO> OrderItems { get; set; }
        public decimal OrderCost { get; set; }
        public decimal ShipmentCost { get; set; }
        public decimal TotalAmount { get; set; }
        public string Address { get; set; }
        public DateTime ShippedOn { get; set; }
        public string ShippingCompany { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
