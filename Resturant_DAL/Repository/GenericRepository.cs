using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Resturant_DAL.DataBase;

namespace Resturant_DAL.Repository
{
    public class GenericRepository<T> where T : class
    {
        private readonly ResturantContext _context;

        public GenericRepository(ResturantContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().Where(filter).ToListAsync();
        }


    }

}


