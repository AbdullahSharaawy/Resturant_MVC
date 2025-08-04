using Microsoft.EntityFrameworkCore;
using Resturant_DAL.DataBase;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant_DAL.ImplementRepository
{
    public class OrderRepo : IRepository<Order>
    {
        private readonly ResturantContext _context;

        public OrderRepo(ResturantContext context)
        {
            _context = context;
        }

        public async Task<int?> Create(Order entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.OrderID;
        }

        public async Task Delete(Order entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAll()
        {
            return await _context.Order
                .Include(o => o.OrderItems) // Include related entities
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Order> GetByID(int id)
        {
            return await _context.Order
                .Include(o => o.OrderItems) // Include related entities
                .FirstOrDefaultAsync(o => o.OrderID == id);
        }

        public async Task Update(Order entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}