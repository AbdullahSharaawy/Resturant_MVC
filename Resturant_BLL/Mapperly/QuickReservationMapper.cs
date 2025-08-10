using Resturant_DAL.Entities;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_DAL.Entities;
using Resturant_BLL.DTOModels.ReservationDTOS;
namespace Resturant_BLL.Mapperly
{
    [Mapper]
    public partial class QuickReservationMapper
    {
        public partial ReservationDTO MapToQuickReservationDTO(Reservation branch);
        public partial Branch MapToQuickReservation(ReservationDTO branch);
        public partial List<ReservationDTO> MapToQuickReservationDTOList(List<Reservation> branches);
    }
}
