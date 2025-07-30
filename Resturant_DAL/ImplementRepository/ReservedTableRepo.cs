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
    public class ReservedTableRepo : IRepository<ReservedTable>
    {
        private readonly ResturantContext _context;
        public ReservedTableRepo(ResturantContext context)
        {
            _context = context;
        }
        public void Create(ReservedTable entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(ReservedTable entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public List<ReservedTable> GetAll()
        {
            return _context.ReservedTable.ToList();
        }

        public ReservedTable GetByID(int id)
        {
            return _context.ReservedTable.FirstOrDefault(c => c.ReservedTableID == id);
        }

        public void Update(ReservedTable entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
