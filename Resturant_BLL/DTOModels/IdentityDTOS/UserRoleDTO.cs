using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels.IdentityDTOS
{
    public class UserRoleDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}
