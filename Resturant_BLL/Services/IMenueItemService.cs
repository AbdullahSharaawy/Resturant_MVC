using System.Collections.Generic;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;

namespace Resturant_BLL.Services
{
    public interface IMenueItemService
    {
        public Task<List<MenueItemDTO>> GetList();
        public Task<MenueItemDTO?> GetById(int id);
        public Task<MenueItemDTO?> Create(MenueItemDTO chief);
        public Task<MenueItemDTO?> Update(MenueItemDTO chief);
        public Task<bool> Delete(int id);
    }
}