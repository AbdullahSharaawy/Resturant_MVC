using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant_BLL.Services
{
    public class BranchService : IBranchService
    {
        private readonly IRepository<Branch> _BR;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> userManager;
        public BranchService(IRepository<Branch> bR, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _BR = bR;
            _httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<Branch?> Create(BranchDTO Branch)
        {
            if (Branch == null)
                return null;
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            Branch mappedBranch = new BranchMapper().MapToBranch(Branch);
            mappedBranch.CreatedOn = DateTime.UtcNow;
            mappedBranch.CreatedBy = $"{user.FirstName} {user.LastName}";
            mappedBranch.IsDeleted = false;
            await _BR.Create(mappedBranch);
            return mappedBranch;
        }

        public async Task<bool> Delete(int id)
        {
            Branch t = await _BR.GetByID(id);
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (t == null || t.IsDeleted == true)
            {
                return false;
            }
            t.IsDeleted = true;
            t.DeletedOn = DateTime.UtcNow;
            t.DeletedBy = $"{user.FirstName} {user.LastName}";
            await _BR.Update(t);
            return true;
        }

        public async Task<BranchDTO?> GetById(int id)
        {
            Branch t = await _BR.GetByID(id);

            if (t == null || t.IsDeleted == true)
            {
                return null;
            }

            BranchDTO BranchDTO = new BranchMapper().MapToBranchDTO(t);
            return BranchDTO;
        }

        public async Task<List<BranchDTO>> GetList()
        {
            List<Branch> Branchs = (await _BR.GetAll()).Where(t => t.IsDeleted == false).ToList();

            if (Branchs == null || Branchs.Count == 0)
            {
                return new List<BranchDTO>();
            }

            List<BranchDTO> BranchsDTO = new BranchMapper().MapToBranchDTOList(Branchs);
            return BranchsDTO;
        }

        public async Task<Branch?> Update(BranchDTO Branch)
        {
            if (Branch == null)
            {
                return null;
            }
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            Branch UpdateBranch = await _BR.GetByID(Branch.BranchID);
            UpdateBranch.Area = Branch.Area;
            UpdateBranch.City = Branch.City;
            UpdateBranch.LocationSelector = Branch.LocationSelector;
            UpdateBranch.BuildingNo = Branch.BuildingNo;
            UpdateBranch.StreetName = Branch.StreetName;
            UpdateBranch.ModifiedOn = DateTime.UtcNow;
            UpdateBranch.ModifiedBy = $"{user.FirstName} {user.LastName}";
            UpdateBranch.IsDeleted = false;
            await _BR.Update(UpdateBranch);
            return UpdateBranch;
        }
    }
}