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
    public class ReviewRepo : IRepository<Review>
    {
        private readonly ResturantContext _context;
        public void Create(Review entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Review entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public List<Review> GetAll()
        {
            return _context.Review.ToList();
        }

        public Review GetByID(int id)
        {
            return _context.Review.FirstOrDefault(a => a.ReviewID == id); 
        }

        public void Update(Review entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
