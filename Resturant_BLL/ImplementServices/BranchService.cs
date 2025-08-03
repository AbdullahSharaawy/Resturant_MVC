using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;

namespace Resturant_BLL.Services
{
    public class BranchService:IBranchService
    {
       
        private readonly IRepository<Branch> _BR;
        public BranchService(IRepository<Branch> bR)
        {
           
            _BR = bR;
        }

        public Branch? Create(BranchDTO Branch)
        {
            if (Branch == null)
                return null;

            Branch mappedBranch = new BranchMapper().MapToBranch(Branch);
            mappedBranch.CreatedOn = DateTime.UtcNow;
            mappedBranch.CreatedBy = "Current User";
            mappedBranch.IsDeleted = false;
            _BR.Create(mappedBranch);
            return mappedBranch;
        }

        public bool Delete(int id)
        {
            Branch t = _BR.GetByID(id);
            if (t == null || t.IsDeleted == true)
            {
                return false;
            }
            t.IsDeleted = true;
            t.DeletedOn = DateTime.UtcNow;
            t.DeletedBy = "Current User"; // This should be replaced with the actual user context
            _BR.Update(t);
            return true;
        }

        public BranchDTO? GetById(int id)
        {
            Branch t = _BR.GetByID(id);

            if (t == null || t.IsDeleted == true)
            {
                return null;
            }

            BranchDTO BranchDTO = new BranchMapper().MapToBranchDTO(t);
            return BranchDTO;
        }

        public List<BranchDTO> GetList()
        {
            List<BranchDTO> BranchsDTO = new List<BranchDTO>();
            List<Branch> Branchs = _BR.GetAll().Where(t => t.IsDeleted == false).ToList();

            if (Branchs == null || Branchs.Count == 0)
            {
                return new List<BranchDTO>();
            }

            BranchsDTO = new BranchMapper().MapToBranchDTOList(Branchs);
            return BranchsDTO;
        }

        public Branch? Update(BranchDTO Branch)
        {
            if (Branch == null)
            {
                return null;
            }


            Branch UpdateBranch = _BR.GetByID(Branch.BranchID);
            UpdateBranch.Area = Branch.Area;
            UpdateBranch.City = Branch.City;
            UpdateBranch.LocationSelector = Branch.LocationSelector;
            UpdateBranch.BuildingNo = Branch.BuildingNo;
            UpdateBranch.StreetName = Branch.StreetName;
            UpdateBranch.ModifiedOn = DateTime.UtcNow;
            UpdateBranch.ModifiedBy = "Current User";
            UpdateBranch.IsDeleted = false;
            _BR.Update(UpdateBranch);
            return UpdateBranch;
        }
    }
}
