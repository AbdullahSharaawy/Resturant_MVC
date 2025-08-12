using Microsoft.EntityFrameworkCore;
using Resturant_DAL.DataBase;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<int?> Create(table entity)
        {
            context.Add(entity);
            await context.SaveChangesAsync();
            return entity.TableID;
        }
        public async Task Delete(table entity)
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<List<table>> GetAll()
        {
            List<table> tables = await context.Table.Where(r => r.IsDeleted == false).ToListAsync();
            foreach (var table in tables)
            {
                await context.Entry(table).Reference(t => t.Branch).LoadAsync();
            }
            return tables;
        }
        public async Task<table> GetByID(int id)
        {
            return await context.Table
                 .Include(t => t.Branch)
                 .Where(r => r.IsDeleted == false)
                 .FirstOrDefaultAsync(t => t.TableID == id);
        }
        public async Task<List<table>> GetAllByFilter(System.Linq.Expressions.Expression<System.Func<table, bool>> filter)
        {
            return await context.Table
                .Where(filter)
                .ToListAsync();
        }
        public async Task Update(table entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}