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
        [Key]
        public int OrderItemID { get; private set; }
        [ForeignKey("Order")]
        public int OrderID { get; private set; }
        [ForeignKey("MenueItem")]
        public int ItemID { get; private set; }
        public int Quantity { get; private set; }
        
        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool IsDeleted { get; private set; }

        public Order Order { get; private set; }
        public MenueItem MenueItem { get; private set; }
    }
}
