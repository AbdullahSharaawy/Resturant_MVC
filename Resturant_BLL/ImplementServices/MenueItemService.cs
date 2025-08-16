using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.Dashboard;
using Resturant_BLL.DTOModels;
using Resturant_BLL.DTOModels.ChifDTOS;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;
using Sharaawy_BL.Helper;

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
            MenuItems = MenuItems.Where(a => !a.IsDeleted).ToList();
            if (MenuItems == null || MenuItems.Count == 0)
            {
                return new List<MenueItemDTO>();
            }
            List<MenueItemDTO> MenuItemDTOs = new MenueItemMapper().MapToMenueItemDTOList(MenuItems);
            return MenuItemDTOs;
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

        public async Task<MenueItemDTO?> Create(MenueItemDTO menueItemDTO)
        {
            if (menueItemDTO == null)
            {
                return null;
            }
            MenueItem menuItem = new MenueItemMapper().MapToMenueItem(menueItemDTO);
            menuItem.CreatedOn = DateTime.UtcNow;
            menuItem.CreatedBy = "Current User";
            menuItem.IsDeleted = false;
            if (menueItemDTO.ImageUrl != null)
                menuItem.ImagePath = Upload.UploadFile("ItemsImages", menueItemDTO.ImageUrl);
            await _CR.Create(menuItem);

            return menueItemDTO;
        }

        public async Task<MenueItemDTO?> Update(MenueItemDTO menueItem)
        {
            if (menueItem == null)
            {
                return null;
            }
            var existingItem = await _CR.GetByID(menueItem.ItemID);
            if (existingItem == null)
                return null;
            existingItem.Name = menueItem.Name;
            existingItem.Description = menueItem.Description;
            existingItem.Price = menueItem.Price;
            existingItem.Category = menueItem.Category;
            existingItem.DietaryInfo = menueItem.DietaryInfo;
            //existingItem.ImageUrl = menueItem.ImageUrl;
            existingItem.ModifiedOn = DateTime.UtcNow;
            existingItem.ModifiedBy = "Current User";
            existingItem.IsDeleted = false;
            if (menueItem.ImageUrl != null)
                existingItem.ImagePath = Upload.UploadFile("ItemsImages", menueItem.ImageUrl);

            await _CR.Update(existingItem);
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