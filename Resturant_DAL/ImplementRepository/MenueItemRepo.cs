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
    public class MenueItemRepo : IRepository<MenueItem>
    {
        private readonly ResturantContext _context;
        public MenueItemRepo(ResturantContext context)
        {
            _context = context;
        }
        public int? Create(MenueItem entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            return entity.ItemID;
        }

        public void Delete(MenueItem entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public List<MenueItem> GetAll()
        {
            return _context.MenueItem.ToList();
        }

        public MenueItem GetByID(int id)
        {
            return _context.MenueItem.FirstOrDefault(a => a.ItemID == id);
        }

        public void Update(MenueItem entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
