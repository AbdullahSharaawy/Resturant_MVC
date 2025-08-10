using System.Collections.Generic;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_BLL.DTOModels.ReservationDTOS;
using Resturant_DAL.Entities;

namespace Resturant_BLL.Services
{
    public interface IReservationService
    {
        public Task<List<ReservationDTO>> GetList();
        public Task<ReservationDTO?> GetById(int id);
      
        public Task<int?> Create(Reservation reservation);
        public Task<(Reservation?, List<ReservedTable>?, Payment?)> CreateQuickReservation(ReservationDTO dto);
        public Task<Reservation?> Update(ReservationDTO reservationDTO);
        public Task<bool> Delete(int id);
        public Task<UpdateReservationDTO?> GetCreateReservationInfo();
        Task<List<ReservationDTO>> GetReservationsByUserId(string userId);
        public Task<bool > FinishQuickReservation(UpdateReservationDTO reservationDTO);

    }
}