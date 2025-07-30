using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_BLL.DTOModels;
using Resturant_DAL.ImplementRepository;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_BLL.Services;
using Resturant_DAL.Entities;
using Resturant_DAL.ImplementRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_DAL.Repository;
namespace Resturant_BLL.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _reservationRepo;

        public ReservationService(IRepository<Reservation> reservationRepo)
        {
            _reservationRepo = reservationRepo;
        }

        public Reservation? Create(ReservationDTO dto)
        {
            if (dto == null)
                return null;

            var reservation = new ReservationMapper().MapToReservation(dto);
            reservation.CreatedOn = DateTime.UtcNow;
            reservation.CreatedBy = "Current User";
            reservation.IsDeleted = false;

            _reservationRepo.Create(reservation);
            return reservation;
        }

        public Reservation? Update(ReservationDTO dto)
        {
            if (dto == null)
                return null;

            var reservation = new ReservationMapper().MapToReservation(dto);
            reservation.ModifiedOn = DateTime.UtcNow;
            reservation.ModifiedBy = "Current User";
            reservation.IsDeleted = false;

            _reservationRepo.Update(reservation);
            return reservation;
        }

        public bool Delete(int id)
        {
            var reservation = _reservationRepo.GetByID(id);
            if (reservation == null || reservation.IsDeleted)
                return false;

            reservation.IsDeleted = true;
            reservation.DeletedOn = DateTime.UtcNow;
            reservation.DeletedBy = "Current User";

            _reservationRepo.Update(reservation);
            return true;
        }

        public ReservationDTO? GetById(int id)
        {
            var reservation = _reservationRepo.GetByID(id);
            if (reservation == null || reservation.IsDeleted)
                return null;

            return new ReservationMapper().MapToReservationDTO(reservation);
        }

        public List<ReservationDTO> GetList()
        {
            var reservations = _reservationRepo.GetAll()
                .Where(r => !r.IsDeleted)
                .ToList();

            return new ReservationMapper().MapToReservationDTOList(reservations);
        }
    }
}
