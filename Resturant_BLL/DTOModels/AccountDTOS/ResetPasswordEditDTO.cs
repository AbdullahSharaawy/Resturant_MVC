using Resturant_DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels.AccountDTOS
{
    public class ResetPasswordEditDTO
    {
        [Required(ErrorMessage = "The New Password is Required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "The Confirm Password is Required")]
        public string ConfirmPassword { get; set; }
        public string userId { get; set; }
        public string token { get; set; }
    }
}
