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
        // [MapProperty(nameof(Department.Id), nameof(DepartmentDTO.ID))]
        //[MapProperty(nameof(Department.Name), nameof(DepartmentDTO.Name))]
        //[MapProperty(nameof(Department.Manager), nameof(DepartmentDTO.Manager))]
        //[MapProperty(nameof(Chief.ChiefID), nameof(ChiefDTO.ChiefID))]
        //[MapProperty(nameof(Chief.Name), nameof(ChiefDTO.Name))]
        //[MapProperty(nameof(Chief.PhoneNumber), nameof(ChiefDTO.PhoneNumber))]
        //[MapProperty(nameof(Chief.Email), nameof(ChiefDTO.Email))]
        //[MapProperty(nameof(Chief.Position), nameof(ChiefDTO.Position))]
        //[MapProperty(nameof(Chief.RestaurantID), nameof(ChiefDTO.RestaurantID))]
        public partial ChiefDTO MapToChiefDTO(Chief chief);
        public partial List<ChiefDTO> MapToChiefDTOList(List<Chief> chiefs);
    }
}
