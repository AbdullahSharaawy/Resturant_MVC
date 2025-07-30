using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;

namespace Resturant_BLL.Services
{
    public class MenueItemService: IMenueItemService
    {
        private readonly IRepository<MenueItem> _CR;

        public MenueItemService(IRepository<MenueItem> rR)
        {
            _CR = rR;
        }
        public List<MenueItemDTO> GetList()
        {
            List<MenueItem> MenuItems = new List<MenueItem>();
            List<MenueItemDTO> MenuItemDTOs = new List<MenueItemDTO>();
            MenuItems = _CR.GetAll();
            if (MenuItems == null || MenuItems.Count == 0)
            {
                return null;
            }
            MenuItemDTOs = new MenueItemMapper().MapToMenueItemDTOList(MenuItems);
            return new List<MenueItemDTO>();
        }
        public MenueItemDTO? GetById(int id)
        {
            MenueItem menuItem = _CR.GetByID(id);
            if (menuItem == null || menuItem.IsDeleted == true)
            {
                return null;
            }
            MenueItemDTO menuItemDTO = new MenueItemMapper().MapToMenueItemDTO(menuItem);
            return menuItemDTO;
        }
        public MenueItemDTO? Create(MenueItemDTO menueItem)
        {
            if (menueItem == null)
            {
                return null;
            }
            MenueItem menuItem = new MenueItemMapper().MapToMenueItem(menueItem);
            menuItem.CreatedOn = DateTime.UtcNow;
            menuItem.CreatedBy = "Current User";
            menuItem.IsDeleted = false;
            _CR.Create(menuItem);
            return menueItem;
        }
        public MenueItemDTO? Update(MenueItemDTO menueItem)
        {
            if (menueItem == null)
            {
                return null;
            }
            MenueItem menuItem = new MenueItemMapper().MapToMenueItem(menueItem);
            menuItem.ModifiedOn = DateTime.UtcNow;
            menuItem.ModifiedBy = "Current User";
            menuItem.IsDeleted = false;
            _CR.Update(menuItem);
            return menueItem;
        }
        public bool Delete(int id)
        {
            MenueItem menuItem = _CR.GetByID(id);
            if (menuItem == null || menuItem.IsDeleted == true)
            {
                return false;
            }
            menuItem.IsDeleted = true;
            menuItem.DeletedOn = DateTime.UtcNow;
            menuItem.DeletedBy = "Current User"; 
            return true;
        }
    }
}
