using Resturant_BLL.DTOModels.BranchDTOS;
using Resturant_BLL.DTOModels.TableDTOS;
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
    public partial class UpdateTableMapper
    {
        [MapProperty(nameof(TableDTO), nameof(UpdateTableDTO.tableDTO))]
        [MapProperty(nameof(List<BranchDTO>), nameof(UpdateTableDTO.Branches))]
        public partial UpdateTableDTO MapToUpdateTableDTO(TableDTO table,List<BranchDTO> branches);
        
    }
}
