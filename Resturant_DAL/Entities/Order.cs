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
        public Order(int orderID, DateTime date, decimal orderCost, decimal shipmentCost, decimal totalAmount, decimal weight, string orderStatus, int paymentID, DateTime createdOn, string createdBy)
        {
            OrderID = orderID;
            Date = date;
            OrderCost = orderCost;
            ShipmentCost = shipmentCost;
            TotalAmount = totalAmount;
            Weight = weight;
            OrderStatus = orderStatus;
            PaymentID = paymentID;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
        }

        [Key]
        public int OrderID { get; private set; }
        public DateTime Date { get; private set; }
        public decimal OrderCost { get; private set; }
        public decimal ShipmentCost { get; private set; }
        public decimal TotalAmount { get; private set; }
        public decimal Weight { get; private set; }
        public string OrderStatus { get; private set; }
        public string Address { get; private set; }

        [ForeignKey("Payment")]
        public int PaymentID { get; private set; }
       
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

        public Payment Payment { get; private set; }
       
        public List<OrderItem> OrderItems { get; private set; }
    }
}
