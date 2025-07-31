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
    public class BranchService : IBranchService
    {
            private readonly IRepository<Branch> _BR;

            public BranchService(IRepository<Branch> br)
            {
                _BR = br;
            }

            public BranchDTO? Create(BranchDTO branch)
            {
                if (branch == null)
                    return null;

                Branch mappedBranch = new BranchMapper().MapToBranch(branch);
                mappedBranch.CreatedOn = DateTime.UtcNow;
                mappedBranch.CreatedBy = "Current User";
                mappedBranch.IsDeleted = false;

                _BR.Create(mappedBranch);
                return branch;
            }

            public bool Delete(int id)
            {
                Branch branch = _BR.GetByID(id);
                if (branch == null || branch.IsDeleted == true)
                {
                    return false;
                }

                branch.IsDeleted = true;
                branch.DeletedOn = DateTime.UtcNow;
                branch.DeletedBy = "Current User";

                _BR.Update(branch);
                return true;
            }

            public BranchDTO? GetById(int id)
            {
                Branch branch = _BR.GetByID(id);
                if (branch == null || branch.IsDeleted == true)
                {
                    return null;
                }

                return new BranchMapper().MapToBranchDTO(branch);
            }

            public List<BranchDTO> GetList()
            {
                List<Branch> branches = _BR.GetAll().Where(b => b.IsDeleted == false).ToList();

                if (branches == null || branches.Count == 0)
                {
                    return new List<BranchDTO>();
                }

                return new BranchMapper().MapToBranchDTOList(branches);
            }

            public BranchDTO? Update(BranchDTO branch)
            {
                if (branch == null)
                    return null;

                Branch mappedBranch = new BranchMapper().MapToBranch(branch);
                mappedBranch.ModifiedOn = DateTime.UtcNow;
                mappedBranch.ModifiedBy = "Current User";
                mappedBranch.IsDeleted = false;

                _BR.Update(mappedBranch);
                return branch;
            }
        }
    }