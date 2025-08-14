using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels.IdentityDTOS
{
    public class UserProfileDTO
    {
        [Required(ErrorMessage = "The First Name is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The Last Name is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please Enter a valid Email.")]

        public string Email { get; set; }
        public string? ImagePath { get; set; }
    }
}
