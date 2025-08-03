using Microsoft.EntityFrameworkCore;
using Resturant_DAL.DataBase;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.ImplementRepository
{
    public class ReservationRepo : IRepository<Reservation>
    {
        private readonly ResturantContext _context;
        public int? Create(Reservation entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            return entity.ReservationID;
        }

        public void Delete(Reservation entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public List<Reservation> GetAll()
        {
            // Eager loading approach (single query - RECOMMENDED)
            List<Reservation> Reservations = _context.Reservation.Where(r => r.IsDeleted == false).ToList();
            foreach (var Reservation in Reservations)
            {
                _context.Entry(Reservation).Reference(t => t.Branch).Load();
            }
            return Reservations;
        }

        public Reservation GetByID(int id)
        {
            return _context.Reservation.Include(r=>r.User).Include(r=>r.Branch).Include(r=>r.Payment).Where(r => r.IsDeleted == false).FirstOrDefault(c => c.ReservationID == id);
        }

        public void Update(Reservation entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
