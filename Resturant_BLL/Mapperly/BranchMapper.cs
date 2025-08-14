using Resturant_BLL.DTOModels.BranchDTOS;
using Resturant_DAL.Entities;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.Mapperly
{
    [Mapper]
    public partial class BranchMapper
    {
        public partial BranchDTO MapToBranchDTO(Branch branch);
        public partial Branch MapToBranch(BranchDTO branch);
        public partial List<BranchDTO> MapToBranchDTOList(List<Branch> branches);
    }
}
