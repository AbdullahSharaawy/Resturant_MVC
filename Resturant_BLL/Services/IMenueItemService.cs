using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;

namespace Resturant_BLL.Services
{
   public interface IMenueItemService
   {
        public List<MenueItemDTO> GetList();
        public MenueItemDTO? GetById(int id);
        public MenueItemDTO? Create(MenueItemDTO chief);
        public MenueItemDTO? Update(MenueItemDTO chief);
        public bool Delete(int id);
   }
}
