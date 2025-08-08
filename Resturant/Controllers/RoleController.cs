using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;

namespace Resturant_PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users=await _userManager.Users.ToListAsync();
            ViewBag.AllRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            List<UserRoleDTO> usersRole=await new UserRoleMapper(_userManager).MapTOUserRoleDTOList(users);
            
            return View("Index",usersRole);
        }
        public async Task<IActionResult> AddRole(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
                return Json(new { success=true,message="The New Role is added Successfuly."});
            }
            return Json(new { success = false, message = "The Role Already Exists." });
            
        }
        public async Task<IActionResult> AssignRole(string userId,string roleName)
        {
            var user=await _userManager.FindByIdAsync(userId);
            var result=await _userManager.AddToRoleAsync(user,roleName);
            if(result==null)
            {
                return Json(new { success = false, message = "Assign Role Failed" });
            }
            return Json(new { success = true, message = "Assing Role is done Successfuly." });
        }
        public async Task<IActionResult> RemoveAllRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Remove all roles
            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if(result != null)
            {
                return Json(new { success = false, message = "Remove All Roles is done Successfuly" });
            }
            return Json(new { success = false, message = "Remove All Roles is Failed" });
        }
        public async Task<IActionResult> RemoveSpecificRoles(string userId,List<string> selectedRoles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Remove all roles
            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);

           
            if (result != null)
            {
              result = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (result != null)
                {
                    return Json(new { success = false, message = "Edit All Roles is done Successfuly" });
                }
            }
            return Json(new { success = false, message = "Edit All Roles is Failed" });
        }
    }
}
