using Resturant_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_DAL.Entities;
using Resturant_DAL.DataBase;

namespace Resturant_DAL.ImplementRepository
{
    public class ResturantRepo : IRepository<Resturant>
    {
        private readonly ResturantContext context;

        public ResturantRepo(ResturantContext context)
        {
            this.context = context;
        }

        public void Create(Resturant entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }

        public void Delete(Resturant entity)
        {
            context.Remove(entity);
            context.SaveChanges();
        }

        public List<Resturant> GetAll()
        {
            return context.Resturant.ToList();
        }

        public Resturant GetByID(int id)
        {
            return context.Resturant.Where(r=>r.RestaurantID==id).FirstOrDefault();
        }

        public void Update(Resturant entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }
    }
}
