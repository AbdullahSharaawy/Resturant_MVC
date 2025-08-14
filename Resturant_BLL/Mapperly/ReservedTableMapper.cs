using Resturant_BLL.DTOModels.ReservedTablesDTOS;
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
        public partial class ReservedTableMapper
        {
            public partial ReservedTableDTO MapToReservedTableDTO(ReservedTable table);
            public partial ReservedTable MapToReservedTable(ReservedTableDTO tableDto);
            public partial List<ReservedTableDTO> MapToReservedTableDTOList(List<ReservedTable> tables);
        }
}
