using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.Entities
{
    public class Order
    {
        public Order() { }
        public Order(int orderID, DateTime date, decimal orderCost, decimal shipmentCost, decimal totalAmount, decimal weight, OrderStatus orderStatus, DateTime createdOn, string createdBy, string userID)
        {
            OrderID = orderID;
            Date = date;
            OrderCost = orderCost;
            ShipmentCost = shipmentCost;
            TotalAmount = totalAmount;
            Weight = weight;
            OrderStatus = orderStatus;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
            UserID = userID;
        }

        [Key]
        public int OrderID { get; private set; }
        public DateTime? Date { get; set; }
        public decimal OrderCost { get; set; }
        public decimal ShipmentCost { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Weight { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string? Address { get; set; }
        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
