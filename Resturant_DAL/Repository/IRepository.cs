using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        public Task<List<T>> GetAll();
        public Task<T> GetByID(int id);
        public Task Update(T entity);
        public Task Delete(T entity);
        public Task<int?> Create(T entity);
    }
}
