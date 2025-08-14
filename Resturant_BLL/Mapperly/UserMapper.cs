using Resturant_BLL.DTOModels.IdentityDTOS;
using Resturant_DAL.Entities;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.Mapperly
{
    [Mapper]
    public partial class UserMapper
    {
        public partial UserProfileDTO MapToUserProfileDTO(User user);
    }
}
