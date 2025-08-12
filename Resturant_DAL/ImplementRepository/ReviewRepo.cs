using Microsoft.EntityFrameworkCore;
using Resturant_DAL.DataBase;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant_DAL.ImplementRepository
{
    public class ReviewRepo : IRepository<Review>
    {
        private readonly ResturantContext _context;

        public ReviewRepo(ResturantContext context)
        {
            _context = context;
        }

        public async Task<int?> Create(Review entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ReviewID;
        }

        public async Task Delete(Review entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Review>> GetAll()
        {
            return await _context.Review.ToListAsync();
        }

        public async Task<Review> GetByID(int id)
        {
            return await _context.Review.FirstOrDefaultAsync(a => a.ReviewID == id);
        }
        public async Task<List<Review>> GetAllByFilter(System.Linq.Expressions.Expression<System.Func<Review, bool>> filter)
        {
            return await _context.Review
                .Include(r => r.User)
                .Where(filter)
                .ToListAsync();
        }

        public async Task Update(Review entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}