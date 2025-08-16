using Microsoft.EntityFrameworkCore;
using Resturant_DAL.DataBase;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Resturant_DAL.ImplementRepository
{
    public class OrderRepo : IRepository<Orders>
    {
        private readonly ResturantContext _context;
        public OrderRepo(ResturantContext context)
        {
            _context = context;
        }
        public async Task<int?> Create(Orders entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.OrderID;
        }
        public async Task Delete(Orders entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Orders>> GetAll()
        {
            return await _context.Orders
                .Include(o => o.OrderItems) // Include related entities
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Orders> GetByID(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems) // Include related entities
                .FirstOrDefaultAsync(o => o.OrderID == id);
        }
        public async Task<List<Orders>> GetAllByFilter(Expression<Func<Orders, bool>> filter)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(o=>o.MenueItem) // Include related entities
                .Where(filter)
                .ToListAsync();
        }
        public async Task Update(Orders entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}