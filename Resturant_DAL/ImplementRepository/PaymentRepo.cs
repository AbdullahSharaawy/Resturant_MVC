using Microsoft.EntityFrameworkCore;
using Resturant_DAL.DataBase;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant_DAL.ImplementRepository
{
    public class PaymentRepo : IRepository<Payment>
    {
        private readonly ResturantContext _context;
        public PaymentRepo(ResturantContext context)
        {
            _context = context;
        }

        public async Task<int?> Create(Payment entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity.PaymentID;
        }

        public async Task Delete(Payment entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Payment>> GetAll()
        {
            return await _context.Payment.ToListAsync();
        }

        public async Task<Payment> GetByID(int id)
        {
            return await _context.Payment.FirstOrDefaultAsync(a => a.PaymentID == id);
        }

        public async Task Update(Payment entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}