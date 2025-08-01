using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;
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
        public Branch? Create(BranchDTO Branch);
        public Branch? Update(BranchDTO Branch);
        public bool Delete(int id);
    }
}
