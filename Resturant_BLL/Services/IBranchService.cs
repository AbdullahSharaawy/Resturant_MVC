using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;

namespace Resturant_BLL.Services
{
    public interface IBranchService
    {
        public List<BranchDTO> GetList();
        public BranchDTO? GetById(int id);
        public BranchDTO? Create(BranchDTO location);
        public BranchDTO? Update(BranchDTO location);
        public bool Delete(int id);
    }
}
