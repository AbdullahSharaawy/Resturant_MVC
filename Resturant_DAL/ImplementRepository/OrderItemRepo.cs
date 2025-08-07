using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Resturant_DAL.DataBase;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant_DAL.ImplementRepository
{
    public class OrderItemRepo : IRepository<OrderItem>
    {
        private readonly ResturantContext _context;

        public OrderItemRepo(ResturantContext context)
        {
            _context = context;
        }

        public async Task<int?> Create(OrderItem entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.OrderItemID;
        }

        public async Task Delete(OrderItem entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderItem>> GetAll()
        {
            return await _context.OrderItem
                .AsNoTracking() // Recommended for read-only operations
                .ToListAsync();
        }

        public async Task<OrderItem> GetByID(int id)
        {
            return await _context.OrderItem
                .AsNoTracking() // Recommended for read-only operations
                .FirstOrDefaultAsync(a => a.OrderItemID == id);
        }
        public async Task<List<OrderItem>> GetAllAsync(System.Linq.Expressions.Expression<System.Func<OrderItem, bool>> filter)
        {
            return await _context.OrderItem
                .Where(filter)
                .ToListAsync();
        }
        public async Task Update(OrderItem entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}