using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels.ChifDTOS
{
    public class ChiefsWithSystemBranchesDTO
    {
        public List<ChiefDTO> chiefsDTO { get; set; }
        public List<BranchDTO> Branches { get; set; }
    }
}
