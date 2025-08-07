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
    public class BranchRepo : IRepository<Branch>
    {
        private readonly ResturantContext _context;
        public BranchRepo(ResturantContext context)
        {
            _context = context;
        }
        public async Task<Branch> GetByID(int id)
        {
            return await _context.Branch
                
                .Where(r => r.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.BranchID == id);
        }

        public async Task<int?> Create(Branch entity)
        {
            if (entity == null)
            {
                return null;
            }

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.BranchID;
        }

        public async Task Delete(Branch entity)
        {
            if (entity == null)
            {
                return ;
            }
             _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Branch>> GetAll()
        {
            return await _context.Branch
                         .Where(r => r.IsDeleted == false)
                         .ToListAsync();
        }
        public async Task<List<Branch>> GetAllAsync(System.Linq.Expressions.Expression<Func<Branch, bool>> filter)
        {
            return await _context.Branch
                .Where(filter)
                .ToListAsync();
        }

        public async Task UpdateAsync(Branch entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public Task Update(Branch entity)
        {
            throw new NotImplementedException();
        }
    }
}