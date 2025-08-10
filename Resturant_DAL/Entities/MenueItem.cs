using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.Entities
{
    public class MenueItem
    {
        public MenueItem() { }
        public MenueItem(int itemID, string name, string description, decimal price, string category, string? dietaryInfo, bool availability, DateTime createdOn, string createdBy)
        {
            ItemID = itemID;
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            DietaryInfo = dietaryInfo;
            Availability = availability;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
        }

        [Key]
        public int ItemID { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string? DietaryInfo { get; set; }
        public bool Availability { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
