using Microsoft.EntityFrameworkCore;
using Resturant_DAL.DataBase;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant_DAL.ImplementRepository
{
    public class ReservedTableRepo : IRepository<ReservedTable>
    {
        private readonly ResturantContext _context;
        public ReservedTableRepo(ResturantContext context)
        {
            _context = context;
        }

        public async Task<int?> Create(ReservedTable entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ReservedTableID;
        }

        public async Task Delete(ReservedTable entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReservedTable>> GetAll()
        {
            return await _context.ReservedTable
                .Where(r => !r.IsDeleted)
                .Include(r => r.Table)
                .Include(r => r.Reservation)
                .ToListAsync();
        }

        public async Task<ReservedTable> GetByID(int id)
        {
            return await _context.ReservedTable
                .Include(r => r.Table)
                .Include(r => r.Reservation)
                .Where(r => r.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.ReservedTableID == id);
        }
        public async Task<List<ReservedTable>> GetAllAsync(System.Linq.Expressions.Expression<System.Func<ReservedTable, bool>> filter)
        {
            return await _context.ReservedTable
                .Where(filter)
                .ToListAsync();
        }
        public async Task Update(ReservedTable entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}