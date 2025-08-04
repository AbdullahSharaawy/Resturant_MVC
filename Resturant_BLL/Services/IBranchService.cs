using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Resturant_BLL.Services
{
    public interface IBranchService
    {
        public Task<List<BranchDTO>> GetList();
        public Task<BranchDTO?> GetById(int id);
        public Task<Branch?> Create(BranchDTO Branch);
        public Task<Branch?> Update(BranchDTO Branch);
        public Task<bool> Delete(int id);
    }
}