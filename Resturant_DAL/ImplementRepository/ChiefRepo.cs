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
    public class ChiefRepo : IRepository<Chief>
    {
        private readonly ResturantContext _context;
        public ChiefRepo(ResturantContext context)
        {
            _context = context;
        }
        public void Create(Chief entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Chief entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public List<Chief> GetAll()
        {
           return _context.Chief.ToList();
        }

        public Chief GetByID(int id)
        {
            return _context.Chief.FirstOrDefault(c => c.ChiefID == id);
        }

        public void Update(Chief entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
