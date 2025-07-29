using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.ImplementRepository;
using Resturant_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.Services
{
    public class ResturantService : IResturantService
    {
        private readonly IRepository<Resturant> _RR;

        public ResturantService(IRepository<Resturant> rR)
        {
            _RR = rR;
        }

        public Resturant? Create(ResturantDTO restaurant)
        {
            if (restaurant == null)
                return null;

            Resturant mappedResturant = new ResturantMapper().MapToResturant(restaurant);
            mappedResturant.CreatedOn = DateTime.UtcNow;
            mappedResturant.CreatedBy = "Current User";
            mappedResturant.IsDeleted = false;
            _RR.Create(mappedResturant);
            return mappedResturant;
        }

        public bool Delete(int id)
        {
            Resturant t = _RR.GetByID(id);
            if (t == null || t.IsDeleted == true)
            {
                return false;
            }
            t.IsDeleted = true;
            t.DeletedOn = DateTime.UtcNow;
            t.DeletedBy = "Current User"; // This should be replaced with the actual user context
            _RR.Update(t);
            return true;
        }

        public ResturantDTO? GetById(int id)
        {
            Resturant t = _RR.GetByID(id);

            if (t == null || t.IsDeleted == true)
            {
                return null;
            }

            ResturantDTO ResturantDTO = new ResturantMapper().MapToResturantDTO(t);
            return ResturantDTO;
        }

        public List<ResturantDTO> GetList()
        {
            List<ResturantDTO> ResturantsDTO = new List<ResturantDTO>();
            List<Resturant> Resturants = _RR.GetAll().Where(t => t.IsDeleted == false).ToList();

            if (Resturants == null || Resturants.Count == 0)
            {
                return new List<ResturantDTO>();
            }

            ResturantsDTO = new ResturantMapper().MapToResturantDTOList(Resturants);
            return ResturantsDTO;
        }

        public Resturant? Update(ResturantDTO restaurant)
        {
            if (restaurant == null)
            {
                return null;
            }


            Resturant mappedResturant = new ResturantMapper().MapToResturant(restaurant);
            mappedResturant.ModifiedOn = DateTime.UtcNow;
            mappedResturant.ModifiedBy = "Current User";
            mappedResturant.IsDeleted = false;
            _RR.Update(mappedResturant);
            return mappedResturant;
        }
    }
}
