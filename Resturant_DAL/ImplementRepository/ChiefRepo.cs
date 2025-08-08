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
    public class ChiefRepo : IRepository<Chief>
    {
        private readonly ResturantContext _context;
        public ChiefRepo(ResturantContext context)
        {
            _context = context;
        }

        public async Task<int?> Create(Chief entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.ChiefID;
        }

        public async Task Delete(Chief entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Chief>> GetAll()
        {
            var chiefs = await _context.Chief
                .Where(r => r.IsDeleted == false)
                .Include(t => t.Branch)  // Using Include for eager loading
                .ToListAsync();

            return chiefs;
        }

        public async Task<Chief> GetByID(int id)
        {
            return await _context.Chief
                .Include(t => t.Branch)
                .Where(r => r.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.ChiefID == id);
        }
        public async Task<List<Chief>> GetAllAsync(System.Linq.Expressions.Expression<Func<Chief, bool>> filter)
        {
            return await _context.Chief
                .Where(filter)
                .ToListAsync();
        }
        public async Task Update(Chief entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}