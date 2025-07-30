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
        public void Create(Reservation entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Reservation entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public List<Reservation> GetAll()
        {
            return _context.Reservation.ToList();
        }

        public Reservation GetByID(int id)
        {
            return _context.Reservation.FirstOrDefault(c => c.ReservationID == id);
        }

        public void Update(Reservation entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
