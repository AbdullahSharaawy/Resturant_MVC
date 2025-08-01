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
    public class BranchRepo : IRepository<Branch>
    {
        private readonly ResturantContext _context;
        public BranchRepo(ResturantContext context)
        {
            _context = context;
        }
        public void Create(Branch entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Branch entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public List<Branch> GetAll()
        {
            List<Branch> Branchs = _context.Branch.ToList();
            
            return Branchs;
        }

        public Branch GetByID(int id)
        {
            return _context.Branch.FirstOrDefault(c => c.BranchID == id);
        }

        public void Update(Branch entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}