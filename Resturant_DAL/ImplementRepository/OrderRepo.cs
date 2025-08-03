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
    public class OrderRepo : IRepository<Order>
    {
        private readonly ResturantContext _context;
        public OrderRepo(ResturantContext context)
        {
            _context = context;
        }

        public int? Create(Order entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            return entity.OrderID;
        }

        public void Delete(Order entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public List<Order> GetAll()
        {
            return _context.Order.ToList();
        }

        public Order GetByID(int id)
        {
            return _context.Order.FirstOrDefault(a => a.OrderID == id);
        }

        public void Update(Order entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
