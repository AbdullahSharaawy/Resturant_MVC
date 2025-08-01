using Resturant_BLL.DTOModels;
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
    public partial class ChiefMapper
    {
        [MapProperty(nameof(Chief.Branch.City), nameof(ChiefDTO.City))]
        [MapProperty(nameof(Chief.Branch.BranchID), nameof(ChiefDTO.BranchID))]
        public partial ChiefDTO MapToChiefDTO(Chief chief);
        public partial Chief MapToChief(ChiefDTO chief);
        public partial List<ChiefDTO> MapToChiefDTOList(List<Chief> chiefs);
    }

}
