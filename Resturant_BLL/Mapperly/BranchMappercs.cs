using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;
using Riok.Mapperly.Abstractions;
using System.Collections.Generic;

namespace Resturant_BLL.Mapperly
{
    [Mapper]
    public partial class BranchMapper
    {
        public partial BranchDTO MapToBranchDTO(Branch branch);
        public partial Branch MapToBranch(BranchDTO branchDTO);
        public partial List<BranchDTO> MapToBranchDTOList(List<Branch> branches);
    }
}