using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels.BranchDTOS;

namespace Resturant_BLL.DTOModels.ChifDTOS
{
    public class ManageChiefDTO
    {
        public ChiefDTO chiefDTO { get; set; }
        public List<BranchDTO>? Branches { get; set; }
    }
}
