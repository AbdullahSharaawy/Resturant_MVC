using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.Entities
{
    public class OrderItem
    {
        public OrderItem(int orderItemID, int orderID, int itemID, int quantity, DateTime createdOn, string createdBy)
        {
            OrderItemID = orderItemID;
            OrderID = orderID;
            ItemID = itemID;
            Quantity = quantity;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
        }

        [Key]
        public int OrderItemID { get; private set; }
        [ForeignKey("Order")]
        public int OrderID { get; private set; }
        [ForeignKey("MenueItem")]
        public int ItemID { get; private set; }
        public int Quantity { get; private set; }
        
        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get;  set; }
        public string? ModifiedBy { get;  set; }
        public DateTime? DeletedOn { get;  set; }
        public string? DeletedBy { get;  set; }
        public bool IsDeleted { get;  set; }

        public Order Order { get; private set; }
        public MenueItem MenueItem { get; private set; }
    }
}
