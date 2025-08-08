using Microsoft.AspNetCore.Identity;
using Resturant_BLL.DTOModels;
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
    public partial class UserRoleMapper
    {
        private readonly UserManager<User> _userManager;

        public UserRoleMapper(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public UserRoleMapper() { }
        public partial  UserRoleDTO MapToUserRoleDTO(User UserRole);
        
        public async Task<List<UserRoleDTO>> MapTOUserRoleDTOList(List<User> users)
        {
            List<UserRoleDTO> userRoleDTOs = new List<UserRoleDTO>();
            foreach (User user in users)
            {
                UserRoleDTO userRoleDTO = new UserRoleDTO();
                userRoleDTO.UserId = user.Id;
                userRoleDTO.UserName = user.UserName;
                userRoleDTO.Email = user.Email;
                userRoleDTO.Roles = (List<string>)await _userManager.GetRolesAsync(user);
                userRoleDTOs.Add(userRoleDTO);
            }
            return userRoleDTOs;
        }
    }

}
