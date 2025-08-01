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
    public class TableRepo : IRepository<table>
    {
        private readonly ResturantContext context;

        public TableRepo(ResturantContext context)
        {
            this.context = context;
        }

        public void Create(table entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }

        public void Delete(table entity)
        {
            context.Remove(entity);
            context.SaveChanges();
        }

        public List<table> GetAll()
        {
            List<table> tables= context.Table.ToList();
            foreach (var table in tables)
            {
                context.Entry(table).Reference(t => t.Branch).Load();
            }
            return tables;
        }

        public table GetByID(int id)
        {
            return context.Table
                 .Include(t => t.Branch)
                 .FirstOrDefault(t => t.TableID == id);
        }

        public void Update(table entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }
    }
}
