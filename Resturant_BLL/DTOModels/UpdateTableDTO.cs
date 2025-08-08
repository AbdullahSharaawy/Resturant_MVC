using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels
{
    public class UpdateTableDTO
    {
        public TableDTO tableDTO { get; set; }
       
        public List<BranchDTO>? Branches { get; set; }
    }
}
