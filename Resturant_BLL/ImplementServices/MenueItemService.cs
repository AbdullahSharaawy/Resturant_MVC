using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;

namespace Resturant_BLL.Services
{
    public class MenueItemService : IMenueItemService
    {
        private readonly IRepository<MenueItem> _CR;

        public MenueItemService(IRepository<MenueItem> rR)
        {
            _CR = rR;
        }

        public async Task<List<MenueItemDTO>> GetList()
        {
            List<MenueItem> MenuItems = await _CR.GetAll();
            if (MenuItems == null || MenuItems.Count == 0)
            {
                return null;
            }
            List<MenueItemDTO> MenuItemDTOs = new MenueItemMapper().MapToMenueItemDTOList(MenuItems);
            return new List<MenueItemDTO>();
        }

        public async Task<MenueItemDTO?> GetById(int id)
        {
            MenueItem menuItem = await _CR.GetByID(id);
            if (menuItem == null || menuItem.IsDeleted == true)
            {
                return null;
            }
            MenueItemDTO menuItemDTO = new MenueItemMapper().MapToMenueItemDTO(menuItem);
            return menuItemDTO;
        }

        public async Task<MenueItemDTO?> Create(MenueItemDTO menueItem)
        {
            if (menueItem == null)
            {
                return null;
            }
            MenueItem menuItem = new MenueItemMapper().MapToMenueItem(menueItem);
            menuItem.CreatedOn = DateTime.UtcNow;
            menuItem.CreatedBy = "Current User";
            menuItem.IsDeleted = false;
            await _CR.Create(menuItem);

            return menueItem;
        }

        public async Task<MenueItemDTO?> Update(MenueItemDTO menueItem)
        {
            if (menueItem == null)
            {
                return null;
            }
            MenueItem menuItem = new MenueItemMapper().MapToMenueItem(menueItem);
            menuItem.ModifiedOn = DateTime.UtcNow;
            menuItem.ModifiedBy = "Current User";
            menuItem.IsDeleted = false;
            await _CR.Update(menuItem);
            return menueItem;
        }

        public async Task<bool> Delete(int id)
        {
            MenueItem menuItem = await _CR.GetByID(id);
            if (menuItem == null || menuItem.IsDeleted == true)
            {
                return false;
            }
            menuItem.IsDeleted = true;
            menuItem.DeletedOn = DateTime.UtcNow;
            menuItem.DeletedBy = "Current User";
            await _CR.Update(menuItem);
            return true;
        }
    }
}