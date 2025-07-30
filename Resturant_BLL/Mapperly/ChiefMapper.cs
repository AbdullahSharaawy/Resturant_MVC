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
     
        public partial ChiefDTO MapToChiefDTO(Chief chief);
        public partial Chief MapToChief(ChiefDTO chief);
        public partial List<ChiefDTO> MapToChiefDTOList(List<Chief> chiefs);
    }

}
