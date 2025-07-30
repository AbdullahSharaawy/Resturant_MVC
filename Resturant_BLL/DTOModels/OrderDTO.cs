using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_DAL.Entities;

namespace Resturant_BLL.DTOModels
{
    public class OrderDTO
    {
        public int OrderID { get; private set; }
        public DateTime Date { get; private set; }
        public List<OrderItemDTO> OrderItems { get; private set; }
        public decimal OrderCost => OrderItems?.Sum(item => item.Price) ?? 0;
        public decimal ShipmentCost { get; private set; }
        public decimal TotalAmount { get; private set; }
        public decimal Weight { get; private set; }
        public string OrderStatus { get; private set; }
    }
}