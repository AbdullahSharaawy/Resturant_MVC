using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.Services
{
    public interface IReservationService
    {
        public List<ReservationDTO> GetList();
        public ReservationDTO? GetById(int id);
        public Reservation? Create(ReservationDTO reservationDTO);
        public int? Create(Reservation reservation);
        public (Reservation?, List<ReservedTable>?,Payment?) CreateQuickReservation(ReservationDTO dto);
        public Reservation? Update(ReservationDTO reservationDTO);
        public bool Delete(int id);
        public UpdateReservationDTO? GetCreateReservationInfo();
    }
}
