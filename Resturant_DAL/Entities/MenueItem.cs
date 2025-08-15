using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenerativeAI.Types;

namespace Resturant_DAL.Entities
{
    public class MenueItem
    {
        public MenueItem() { }
        public MenueItem(int itemID, string name, string description, decimal price, string category, string? dietaryInfo, bool availability, DateTime createdOn, string createdBy, string? imagePath)
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
            ImagePath = imagePath;
        }

        [Key]
        public int ItemID { get; private set; }
        [Required(ErrorMessage = "Item Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [MinLength(15,ErrorMessage = "Description should be more than 20 character")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Category is required")]
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
        public string? ImagePath { get; set; } = "cake.jpg";
    }
}
