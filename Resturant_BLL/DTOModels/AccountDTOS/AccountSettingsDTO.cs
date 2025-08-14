using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels.AccountDTOS
{
    public class AccountSettingsDTO
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public bool EmailNotification { get; set; }
        public bool PromoEmails { get; set; }
    }
}
