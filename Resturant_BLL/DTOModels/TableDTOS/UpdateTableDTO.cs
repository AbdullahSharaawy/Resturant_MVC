using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels.BranchDTOS;

namespace Resturant_BLL.DTOModels.TableDTOS
{
    public class UpdateTableDTO
    {
        public TableDTO tableDTO { get; set; }

        public List<BranchDTO>? Branches { get; set; }
    }
}
