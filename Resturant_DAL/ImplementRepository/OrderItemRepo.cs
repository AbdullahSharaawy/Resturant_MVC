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
    public class OrderItemRepo : IRepository<OrderItem>
    {
        private readonly ResturantContext _context;
        public OrderItemRepo(ResturantContext context)
        {
            _context = context;
        }
        public void Create(OrderItem entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(OrderItem entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public List<OrderItem> GetAll()
        {
            return _context.OrderItem.ToList();
        }

        public OrderItem GetByID(int id)
        {
            return _context.OrderItem.FirstOrDefault(a => a.OrderItemID == id);
        }

        public void Update(OrderItem entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
