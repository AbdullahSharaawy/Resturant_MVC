using DAL = Resturant_DAL.Entities;
using DTO = Resturant_BLL.DTOModels;
using Riok.Mapperly.Abstractions;
using System.Collections.Generic;

namespace Resturant_BLL.Mapperly
{
    [Mapper]
    public partial class ReservationMapper
    {
        public partial DTO.ReservationDTO MapToReservationDTO(DAL.Reservation entity);
        public partial DAL.Reservation MapToReservation(DTO.ReservationDTO dto);
        public partial List<DTO.ReservationDTO> MapToReservationDTOList(List<DAL.Reservation> reservations);
    }
}




