using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        public List<T> GetAll();
        public T GetByID(int id);
        public void Update(T entity);
        public void Delete(T entity);
        public int? Create(T entity);
    }
}
