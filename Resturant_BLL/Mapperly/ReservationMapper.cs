using DAL = Resturant_DAL.Entities;
using DTO = Resturant_BLL.DTOModels;
using Riok.Mapperly.Abstractions;
using System.Collections.Generic;
using Resturant_BLL.DTOModels.ReservationDTOS;

namespace Resturant_BLL.Mapperly
{
    [Mapper]
    public partial class ReservationMapper
    {
        public partial ReservationDTO MapToReservationDTO(DAL.Reservation entity);
        public partial DAL.Reservation MapToReservation(ReservationDTO dto);
        public partial List<ReservationDTO> MapToReservationDTOList(List<DAL.Reservation> reservations);
    }
}




