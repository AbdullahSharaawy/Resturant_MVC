using Resturant_BLL.DTOModels;
using Resturant_BLL.DTOModels.ChifDTOS;
using Resturant_BLL.DTOModels.ReviewDTOS;
using Resturant_BLL.Mapperly;
using Resturant_BLL.Services;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;
using Sharaawy_BL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public async Task<Chief?> Create(ChiefDTO chiefDTO)
        {
            if (chiefDTO == null)
                return null;

            Chief NewChief = new ChiefMapper().MapToChief(chiefDTO);
            NewChief.CreatedOn = DateTime.UtcNow;
            NewChief.CreatedBy = "Current User";
            NewChief.IsDeleted = false;
            if (chiefDTO.ImageUrl != null)
                NewChief.ImagePath = Upload.UploadFile("UserImages", chiefDTO.ImageUrl);
            await _CR.Create(NewChief);
            return NewChief;
        }

        public async Task<bool> Delete(int id)
        {
            Chief t = await _CR.GetByID(id);
            if (t == null || t.IsDeleted == true)
            {
                return false;
            }
            t.IsDeleted = true;
            t.DeletedOn = DateTime.UtcNow;
            t.DeletedBy = "Current User";
            await _CR.Update(t);
            return true;
        }

        public async Task<ChiefDTO?> GetById(int id)
        {
            Chief t = await _CR.GetByID(id);

            if (t == null || t.IsDeleted == true)
            {
                return null;
            }

            ChiefDTO ChiefDTO = new ChiefMapper().MapToChiefDTO(t);
            return ChiefDTO;
        }

        public async Task<ManageChiefDTO?> GetCreateChiefInfo()
        {
            ManageChiefDTO createChiefDTO = new ManageChiefDTO();
            createChiefDTO.Branches = (await _BR.GetAll())
                .Where(b => b.IsDeleted == false)
                .Select(b => new BranchMapper().MapToBranchDTO(b))
                .ToList();
            return createChiefDTO;
        }
        public async Task<List<ChiefDTO>> GetList(Expression<Func<Chief, bool>> filter)
        {
            List<Chief> chiefs = await _CR.GetAllByFilter(filter);

            if (chiefs == null || chiefs.Count == 0)
            {
                return new List<ChiefDTO>();
            }

            return new ChiefMapper().MapToChiefDTOList(chiefs);
        }
        public async Task<List<ChiefDTO>> GetList()
        {
            List<Chief> Chiefs = (await _CR.GetAll()).Where(t => t.IsDeleted == false).ToList();

            if (Chiefs == null || Chiefs.Count == 0)
            {
                return new List<ChiefDTO>();
            }

            List<ChiefDTO> ChiefsDTO = new ChiefMapper().MapToChiefDTOList(Chiefs);
            return ChiefsDTO;
        }

        public async Task<ManageChiefDTO?> GetUpdateChiefInfo(int id)
        {
            ChiefDTO chiefDTO = await GetById(id);
            if (chiefDTO == null)
            {
                return null;
            }
            List<BranchDTO> Branches = (await _BR.GetAll()).Where(b => b.IsDeleted == false).Select(b => new BranchMapper().MapToBranchDTO(b)).ToList();
            if (Branches == null || Branches.Count == 0)
            {
                return null;
            }
            ManageChiefDTO updateChiefDTO = new ManageChiefDTO();
            updateChiefDTO.Branches = Branches;
            updateChiefDTO.chiefDTO = chiefDTO;
            return updateChiefDTO;
        }

        public async Task<Chief?> Update(ChiefDTO chiefDTO)
        {
            if (chiefDTO == null)
            {
                return null;
            }

            Chief UpdatedChief = await _CR.GetByID(chiefDTO.ChiefID);
            UpdatedChief.PhoneNumber = chiefDTO.PhoneNumber;
            UpdatedChief.Name = chiefDTO.Name;
            UpdatedChief.Email = chiefDTO.Email;
            UpdatedChief.Position = chiefDTO.Position;
            UpdatedChief.BranchID = chiefDTO.BranchID;
            UpdatedChief.ModifiedOn = DateTime.UtcNow;
            UpdatedChief.ModifiedBy = "Current User";
            if (chiefDTO.ImageUrl != null)
                UpdatedChief.ImagePath = Upload.UploadFile("UserImages", chiefDTO.ImageUrl);

            await _CR.Update(UpdatedChief);
            return UpdatedChief;
        }
    }
}