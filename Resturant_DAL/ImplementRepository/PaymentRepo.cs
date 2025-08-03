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
    public class PaymentRepo : IRepository<Payment>
    {
        private readonly ResturantContext _context;
        public PaymentRepo(ResturantContext context)
        {
            _context = context;
        }
        public int? Create(Payment entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            return entity.PaymentID;
        }

        public void Delete(Payment entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public List<Payment> GetAll()
        {
            return _context.Payment.ToList();
        }

        public Payment GetByID(int id)
        {
            return _context.Payment.FirstOrDefault(a => a.PaymentID == id);
        }

        public void Update(Payment entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}