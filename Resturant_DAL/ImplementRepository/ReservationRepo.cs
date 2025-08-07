using Microsoft.EntityFrameworkCore;
using Resturant_DAL.DataBase;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant_DAL.ImplementRepository
{
    public class ReservationRepo : IRepository<Reservation>
    {
        private readonly ResturantContext _context;

        public ReservationRepo(ResturantContext context)
        {
            _context = context;
        }

        public async Task<int?> Create(Reservation entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ReservationID;
        }

        public async Task Delete(Reservation entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Reservation>> GetAll()
        {
            List<Reservation> reservations = await _context.Reservation
                .Where(r => r.IsDeleted == false)
                .ToListAsync();

            foreach (var reservation in reservations)
            {
                await _context.Entry(reservation).Reference(t => t.Branch).LoadAsync();
            }
            return reservations;
        }

        public async Task<Reservation> GetByID(int id)
        {
            return await _context.Reservation
                .Include(r => r.User)
                .Include(r => r.Branch)
                .Include(r => r.Payment)
                .Where(r => r.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.ReservationID == id);
        }
        public async Task<List<Reservation>> GetAllAsync(System.Linq.Expressions.Expression<System.Func<Reservation, bool>> filter)
        {
            return await _context.Reservation
                .Where(filter)
                .ToListAsync();
        }
        public async Task Update(Reservation entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}