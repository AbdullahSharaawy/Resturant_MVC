using Microsoft.EntityFrameworkCore;
using Resturant_DAL.DataBase;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant_DAL.ImplementRepository
{
    public class MenueItemRepo : IRepository<MenueItem>
    {
        private readonly ResturantContext _context;
        public MenueItemRepo(ResturantContext context)
        {
            _context = context;
        }

        public async Task<int?> Create(MenueItem entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.ItemID;
        }

        public async Task Delete(MenueItem entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MenueItem>> GetAll()
        {
            return await _context.MenueItem.ToListAsync();
        }

        public async Task<MenueItem> GetByID(int id)
        {
            return await _context.MenueItem.FirstOrDefaultAsync(a => a.ItemID == id);
        }

        public async Task<List<MenueItem>> GetAllAsync(System.Linq.Expressions.Expression<System.Func<MenueItem, bool>> filter)
        {
            return await _context.MenueItem
                .Where(filter)
                .ToListAsync();
        }
        public async Task Update(MenueItem entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}