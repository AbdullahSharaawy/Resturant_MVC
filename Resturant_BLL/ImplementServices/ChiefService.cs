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
        private readonly IRepository<Branch> _BR;
        public ChiefService(IRepository<Chief> rR, IRepository<Branch> bR)
        {
            _CR = rR;
            _BR = bR;
        }

        public Chief? Create(ChiefDTO chiefDTO)  
        {
            if (chiefDTO == null)
                return null;

            Chief mappedChief = new ChiefMapper().MapToChief(chiefDTO);
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

        

        public UpdateChiefDTO? GetCreateChiefInfo()
        {
            UpdateChiefDTO createChiefDTO = new UpdateChiefDTO();
            createChiefDTO.Branches = _BR.GetAll()
                .Where(b => b.IsDeleted == false)
                .Select(b => new BranchMapper().MapToBranchDTO(b))
                .ToList();
            return createChiefDTO;
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

        public UpdateChiefDTO? GetUpdateChiefInfo(int id)
        {
            ChiefDTO chiefDTO = GetById(id);
            if (chiefDTO == null)
            {
                return null;
            }
            List<BranchDTO> Branches = _BR.GetAll().Where(b => b.IsDeleted == false).Select(b => new BranchMapper().MapToBranchDTO(b)).ToList();
            if (Branches == null || Branches.Count == 0)
            {
                return null;
            }
            // i can`t apply mapperly here because it will not work with the list of branches
            UpdateChiefDTO updateChiefDTO = new UpdateChiefDTO();
            updateChiefDTO.Branches = Branches;
            updateChiefDTO.chiefDTO = chiefDTO;
            return updateChiefDTO;
        }

        public Chief? Update(ChiefDTO chiefDTO)
        {
            if (chiefDTO == null)
            {
                return null;
            }

            // the mapping remove the values that are not in the DTO
            Chief UpdatedChief = _CR.GetByID(chiefDTO.ChiefID);
            UpdatedChief.PhoneNumber=chiefDTO.PhoneNumber;
            UpdatedChief.Name = chiefDTO.Name;
            UpdatedChief.Email = chiefDTO.Email;
            UpdatedChief.Position = chiefDTO.Position;
            UpdatedChief.BranchID = chiefDTO.BranchID;
            UpdatedChief.ModifiedOn = DateTime.UtcNow;
            UpdatedChief.ModifiedBy = "Current User"; // This should be replaced with the actual user context
            _CR.Update(UpdatedChief);
            return UpdatedChief;
        }
    }
}
