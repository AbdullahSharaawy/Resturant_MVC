using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;

namespace Resturant_BLL.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _reservationRepo;
        private readonly IRepository<table> _tableRepo;
        private readonly IRepository<ReservedTable> _reservedTableRepo;
        private readonly IRepository<Branch> _branchRepo;

        public ReservationService(IRepository<Reservation> reservationRepo,
                                IRepository<table> tableRepo,
                                IRepository<ReservedTable> reservedTableRepo,
                                IRepository<Branch> branchRepo)
        {
            _reservationRepo = reservationRepo;
            _tableRepo = tableRepo;
            _reservedTableRepo = reservedTableRepo;
            _branchRepo = branchRepo;
        }

        public async Task<Reservation?> Create(ReservationDTO dto)
        {
            if (dto == null)
                return null;

            Reservation mappedReservation = new ReservationMapper().MapToReservation(dto);
            mappedReservation.CreatedOn = DateTime.UtcNow;
            mappedReservation.CreatedBy = "Current User";
            mappedReservation.IsDeleted = false;
            await _reservationRepo.Create(mappedReservation);
            return mappedReservation;
        }

        public async Task<(Reservation?, List<ReservedTable>?, Payment)> CreateQuickReservation(ReservationDTO dto)
        {
            if (dto == null) return (null, null, null);

            // Get all tables and reserved tables first
            var allTables = await _tableRepo.GetAll();
            var allReservedTables = await _reservedTableRepo.GetAll();

            // Get available tables at the specified time and branch
            var availableTables = allTables
                .Where(t => t.BranchID == dto.BranchID && !t.IsDeleted)
                .Where(t => !allReservedTables
                    .Any(rt => rt.DateTime == dto.DateTime && rt.TableID == t.TableID))
                .OrderBy(t => t.Capacity)
                .ToList();

            // Rest of the method remains the same...
            var selectedTables = FindOptimalTables(availableTables, dto.NumberOfGuests);

            if (selectedTables.Count == 0)
            {
                return (null, null, null);
            }

            var reservation = new ReservationMapper().MapToReservation(dto);
            reservation.Cost = reservation.NumberOfGuests * 5;
            reservation.CreatedOn = DateTime.UtcNow;
            reservation.CreatedBy = "Current User";
            reservation.IsDeleted = false;
            reservation.Status = "Completed";

            List<ReservedTable> reservedTables = new List<ReservedTable>();
            foreach (var table in selectedTables)
            {
                var resrvedTable = new ReservedTable
                {
                    TableID = table.TableID,
                    ReservationID = reservation.ReservationID,
                    DateTime = dto.DateTime,
                    CreatedBy = "user",
                    CreatedOn = DateTime.UtcNow,
                };
                reservedTables.Add(resrvedTable);
            }

            Payment payment = new Payment
            {
                Amount = reservation.Cost,
                Status = "Completed",
                PaymentMethod = PaymentMethod.Paypal,
                CreatedBy = "user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                Date = DateTime.UtcNow,
            };

            return (reservation, reservedTables, payment);
        }
        private List<table> FindOptimalTables(List<table> availableTables, int requiredCapacity)
        {
            // Try to find a single table first
            var singleTable = availableTables.FirstOrDefault(t => t.Capacity >= requiredCapacity);
            if (singleTable != null)
            {
                return new List<table> { singleTable };
            }

            // Find the smallest combination of tables that meets the requirement
            var combinations = new List<List<table>>();
            FindTableCombinations(availableTables, requiredCapacity, 0, new List<table>(), combinations);

            return combinations
                .OrderBy(c => c.Sum(t => t.Capacity)) // Prefer combinations closest to required capacity
                .ThenBy(c => c.Count) // Prefer fewer tables
                .FirstOrDefault() ?? new List<table>();
        }

        private void FindTableCombinations(List<table> tables, int remaining, int start,
                                         List<table> current, List<List<table>> results)
        {
            if (remaining <= 0)
            {
                results.Add(new List<table>(current));
                return;
            }

            for (int i = start; i < tables.Count; i++)
            {
                current.Add(tables[i]);
                FindTableCombinations(tables, remaining - tables[i].Capacity, i + 1, current, results);
                current.RemoveAt(current.Count - 1);
            }
        }

        public async Task<UpdateReservationDTO?> GetCreateReservationInfo()
        {
            UpdateReservationDTO createChiefDTO = new UpdateReservationDTO();
            createChiefDTO.Branches = (await _branchRepo.GetAll())
                .Where(b => b.IsDeleted == false)
                .Select(b => new BranchMapper().MapToBranchDTO(b))
                .ToList();
            return createChiefDTO;
        }

        public async Task<Reservation?> Update(ReservationDTO dto)
        {
            if (dto == null)
                return null;

            var reservation = new ReservationMapper().MapToReservation(dto);
            reservation.ModifiedOn = DateTime.UtcNow;
            reservation.ModifiedBy = "Current User";
            reservation.IsDeleted = false;

            await _reservationRepo.Update(reservation);
            return reservation;
        }

        public async Task<bool> Delete(int id)
        {
            var reservation = await _reservationRepo.GetByID(id);
            if (reservation == null || reservation.IsDeleted)
                return false;

            reservation.IsDeleted = true;
            reservation.DeletedOn = DateTime.UtcNow;
            reservation.DeletedBy = "Current User";

            await _reservationRepo.Update(reservation);
            return true;
        }

        public async Task<ReservationDTO?> GetById(int id)
        {
            var reservation = await _reservationRepo.GetByID(id);
            if (reservation == null || reservation.IsDeleted)
                return null;

            return new ReservationMapper().MapToReservationDTO(reservation);
        }

        public async Task<List<ReservationDTO>> GetList()
        {
            var reservations = (await _reservationRepo.GetAll())
                .Where(r => !r.IsDeleted)
                .ToList();

            return new ReservationMapper().MapToReservationDTOList(reservations);
        }

        public async Task<int?> Create(Reservation reservation)
        {
            return await _reservationRepo.Create(reservation);
        }
    }
}