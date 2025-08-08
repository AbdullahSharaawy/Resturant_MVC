using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels
{
    public class UpdateChiefDTO
    {
        public ChiefDTO chiefDTO { get; set; }
        public List<BranchDTO>? Branches { get; set; }
    }
}
