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
        [Key]
        public int OrderID { get; private set; }
        public DateTime Date { get; private set; }
        public decimal OrderCost { get; private set; }
        public decimal ShipmentCost { get; private set; }
        public decimal TotalAmount { get; private set; }
        public decimal Weight { get; private set; }
        public string OrderStatus { get; private set; }

        [ForeignKey("Payment")]
        public int PaymentID { get; private set; }
       
        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool IsDeleted { get; private set; }

        public Payment? Payment { get; private set; }
       
        public List<OrderItem> OrderItems { get; private set; }
    }
}
