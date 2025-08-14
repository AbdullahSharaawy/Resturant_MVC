using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Resturant_BLL.DTOModels
{
    public class MenueItemDTO
    {
        public int ItemID { get;  set; }
        [Required(ErrorMessage = "Item Name is required")]
        public string Name { get;  set; }
        [Required(ErrorMessage = "Description is required")]
        [MinLength(15, ErrorMessage = "Description should be more than 20 character")]
        public string Description { get;  set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get;  set; }
        [Required(ErrorMessage = "Category is required")]
        public string Category { get;  set; }
        public string? DietaryInfo { get;  set; }
        public bool Availability { get;  set; }
        public string? ImagePath { get; set; } = "PersonIcon.svg";
        public IFormFile? ImageUrl { get; set; }
    }
}
