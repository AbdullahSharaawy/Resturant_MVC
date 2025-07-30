using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.ImplementRepository;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_BLL.Services;
using Resturant_DAL.Entities;
using Resturant_DAL.ImplementRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_DAL.Repository;

namespace Chief_BLL.Services
{
    public class ChiefService : IChiefService
    {
        private readonly IRepository<Chief> _CR;

        public ChiefService(IRepository<Chief> rR)
        {
            _CR = rR;
        }

        public Chief? Create(ChiefDTO restaurant)  
        {
            if (restaurant == null)
                return null;

            Chief mappedChief = new ChiefMapper().MapToChief(restaurant);
            mappedChief.CreatedOn = DateTime.UtcNow;
            mappedChief.CreatedBy = "Current User";
            mappedChief.IsDeleted = false;
            _CR.Create(mappedChief);
            return mappedChief;
        }

        public bool Delete(int id)
        {
            Chief t = _CR.GetByID(id);
            if (t == null || t.IsDeleted == true)
            {
                return false;
            }
            t.IsDeleted = true;
            t.DeletedOn = DateTime.UtcNow;
            t.DeletedBy = "Current User"; // This should be replaced with the actual user context
            _CR.Delete(t);
            return true;
        }

        public ChiefDTO? GetById(int id)
        {
            Chief t = _CR.GetByID(id);

            if (t == null || t.IsDeleted == true)
            {
                return null;
            }

            ChiefDTO ChiefDTO = new ChiefMapper().MapToChiefDTO(t);
            return ChiefDTO;
        }

        public List<ChiefDTO> GetList()
        {
            List<ChiefDTO> ChiefsDTO = new List<ChiefDTO>();
            List<Chief> Chiefs = _CR.GetAll().Where(t => t.IsDeleted == false).ToList();

            if (Chiefs == null || Chiefs.Count == 0)
            {
                return new List<ChiefDTO>();
            }

            ChiefsDTO = new ChiefMapper().MapToChiefDTOList(Chiefs);
            return ChiefsDTO;
        }

        public Chief? Update(ChiefDTO restaurant)
        {
            if (restaurant == null)
            {
                return null;
            }
            Chief mappedChief = new ChiefMapper().MapToChief(restaurant);
            mappedChief.ModifiedOn = DateTime.UtcNow;
            mappedChief.ModifiedBy = "Current User";
            mappedChief.IsDeleted = false;
            _CR.Update(mappedChief);
            return mappedChief;
        }
    }
}
