using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;
using AutoMapper;
using Castle.Core.Configuration;
using Resturant_BLL.DTOModels.ReservationDTOS;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Hangfire;
using Resturant_BLL.ImplementServices;
using Microsoft.Extensions.Options;

namespace Resturant_BLL.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _RR;
        private readonly IRepository<table> _TR;
        private readonly IRepository<ReservedTable> _RTR;
        private readonly IRepository<Branch> _BR;
        private readonly IReservedTableService _RTS;
        private readonly IPaymentService _PS;
        private readonly IBranchService _BS;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSenderService _ESS;
        private readonly EmailSettings _emailSettings;
      
        public ReservationService(IRepository<Reservation> reservationRepo,
                           IRepository<table> tableRepo,
                           IRepository<ReservedTable> reservedTableRepo,
                           IRepository<Branch> branchRepo,
                           IReservedTableService rTS,
                           IPaymentService pS,
                           UserManager<User> userManager,
                           IBranchService bS,
                           IHttpContextAccessor httpContextAccessor,
                           IEmailSenderService eSS, IOptions<EmailSettings> emailSettings)
        {
            _RR = reservationRepo;
            _TR = tableRepo;
            _RTR = reservedTableRepo;
            _BR = branchRepo;
            _RTS = rTS;
            _PS = pS;
            this.userManager = userManager;
            _BS = bS;
            _httpContextAccessor = httpContextAccessor;
            _ESS = eSS;
            _emailSettings = emailSettings.Value;
           
           
        }


        private async Task<Reservation?> Create(ReservationDTO dto)
        {
            if (dto == null)
                return null;

            var reservation = new ReservationMapper().MapToReservation(dto);
           
            reservation.CreatedOn = DateTime.UtcNow;

            reservation.IsDeleted = false;
            reservation.CreatedBy = dto.CreatedBy;

            return reservation;
        }

        public async Task<(Reservation?, List<ReservedTable>?, Payment)> CreateQuickReservation(ReservationDTO dto)
        {
            if (dto == null) return (null, null, null);

            // Get all tables and reserved tables first
            // Get available tables at the specified time and branch
            var availableTables =await AvailableTables( dto);

            // Rest of the method remains the same...
            var selectedTables = FindOptimalTables(availableTables, dto.NumberOfGuests);

            if (selectedTables.Count == 0)
            {
                return (null, null, null);
            }
            // create object without save in database
            var reservation =await Create(dto);
            reservation.SecretKey = GenerateSecretKey();
            List<ReservedTable> reservedTables = new List<ReservedTable>();
            foreach (var table in selectedTables)
            {
                var resrvedTable = new ReservedTable
                {
                    TableID = table.TableID,
                    ReservationID = reservation.ReservationID,
                    DateTime = dto.DateTime,
                    CreatedBy = dto.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                };
                reservedTables.Add(resrvedTable);
            }

             Payment payment = new Payment
            {
                Amount = dto.NumberOfGuests*5,
                Status = "Progress",
                PaymentMethod="Cash",
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                Date = dto.DateTime,
            };

            return (reservation, reservedTables, payment);
        }
        private async Task<List<table>> AvailableTables(ReservationDTO dto)
        {
            var allTables = await _TR.GetAll();
            var allReservedTables = await _RTR.GetAll();

            // Get available tables at the specified time and branch
            var availableTables = allTables
                .Where(t => t.BranchID == dto.BranchID && !t.IsDeleted)
                .Where(t => !allReservedTables
                    .Any(rt => rt.DateTime == dto.DateTime && rt.TableID == t.TableID))
                .OrderBy(t => t.Capacity)
                .ToList();
            return availableTables;
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
            createChiefDTO.Branches = (await _BR.GetAll())
                .Where(b => b.IsDeleted == false)
                .Select(b => new BranchMapper().MapToBranchDTO(b))
                .ToList();
            return createChiefDTO;
        }

        public async Task<Reservation?> Update(ReservationDTO dto)
        {
            if (dto == null)
                return null;
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            Reservation oldReservation=await _RR.GetByID(dto.ReservationID);
            List<ReservedTable> allReservedTables = await _RTR.GetAll();
            // delete the reserved tables from database for this reservation
            List<ReservedTable> targetReservedTables = allReservedTables.Where(b => b.IsDeleted == false)
                .Where(b=>b.DateTime== oldReservation.DateTime).ToList();
           
            foreach (var table in targetReservedTables)
            {
              await _RTS.Delete(table);
            }
            // get availabe tebes for the update reservation
            List<table> availableTables = await AvailableTables(dto);
            var selectedTables = FindOptimalTables(availableTables, dto.NumberOfGuests);
            // return the old availabe tables to the database with new id
            // the update operation is failed
            if (selectedTables.Count == 0 || selectedTables == null)
            {
                foreach (var table in targetReservedTables)
                {
                    await _RTS.Create(table);
                }
                return null;
            }
            List<ReservedTable> reservedTables = new List<ReservedTable>();
            foreach (var table in selectedTables)
            {
                var resrvedTable = new ReservedTable
                {
                    TableID = table.TableID,
                    ReservationID = dto.ReservationID,
                    DateTime = dto.DateTime,
                    CreatedBy = user.FirstName+" "+user.LastName,
                    CreatedOn = DateTime.UtcNow,
                };
                reservedTables.Add(resrvedTable);
            }
            // insert to the database the new reserved tables records based on the update
            foreach (var reservedTable in reservedTables)
            {
                reservedTable.ReservationID = dto.ReservationID;
                await _RTS.Create(reservedTable);
            }
            // update the payment
            PaymentDTO payement = new PaymentDTO
            {
                Amount = dto.NumberOfGuests * 5,
                PaymentMethod="Cash",
                Date=dto.DateTime,
                PaymentID=dto.PaymentID,
                Status="Progress"
            };
            await _PS.Update(payement);

           
           
            oldReservation.BranchID = dto.BranchID;
            oldReservation.NumberOfGuests = dto.NumberOfGuests;
            oldReservation.DateTime = dto.DateTime;
            oldReservation.ModifiedOn = DateTime.UtcNow;
            oldReservation.ModifiedBy = user.FirstName + " " + user.LastName;
            oldReservation.IsDeleted = false;

            await _RR.Update(oldReservation);
            return oldReservation;
        }

        public async Task<bool> Delete(int id)
        {
            var reservation = await _RR.GetByID(id);
           
            if (reservation == null || reservation.IsDeleted)
                return false;

            if(await _PS.Delete(reservation.PaymentID))
            {
                foreach(var reservedTable in reservation.ReservedTables)
                {
                    if(! await _RTS.Delete(reservedTable.ReservedTableID))
                        return false;
                }
            }
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            reservation.IsDeleted = true;
            reservation.DeletedOn = DateTime.UtcNow;
            reservation.DeletedBy = user.FirstName + " " + user.LastName;

            await _RR.Update(reservation);
            return true;
        }

        public async Task<ReservationDTO?> GetById(int id)
        {
            var reservation = await _RR.GetByID(id);
            if (reservation == null || reservation.IsDeleted)
                return null;

            return new ReservationMapper().MapToReservationDTO(reservation);
        }

        public async Task<List<ReservationDTO>> GetList()
        {
            var reservations = (await _RR.GetAll())
                .Where(r => !r.IsDeleted)
                .ToList();

            return new ReservationMapper().MapToReservationDTOList(reservations);
        }

        public async Task<int?> Create(Reservation reservation)
        {
            return await _RR.Create(reservation);
        }
        public async Task<List<ReservationDTO>> GetReservationsByUserId(string userId)
        {
            var reservations = await _RR.GetAllByFilter(r => r.UserID == userId && r.IsDeleted==false);

            return new ReservationMapper().MapToReservationDTOList(reservations);

        }

        public async Task<bool> FinishQuickReservation(UpdateReservationDTO updateReservationDTO)
        {
            CheckOutDTO checkOutDTO = new CheckOutDTO();
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            updateReservationDTO.ReservationDTO.CreatedBy = user.FirstName + " " + user.LastName;
            var quickReservationResult = await CreateQuickReservation(updateReservationDTO.ReservationDTO);
            checkOutDTO.reservation = quickReservationResult.Item1;
            checkOutDTO.reservedTable = quickReservationResult.Item2;
            checkOutDTO.Payment = quickReservationResult.Item3;

            if ((checkOutDTO.reservation, checkOutDTO.reservedTable, checkOutDTO.Payment) == (null, null, null))
            {
                updateReservationDTO.Branches = await _BS.GetList();
                return  false;
            }

            int paymentID = (await _PS.Create(checkOutDTO.Payment)) ?? 0;
            if (paymentID != 0)
            {
                checkOutDTO.reservation.PaymentID = paymentID;
                checkOutDTO.reservation.UserID = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                int reservationID = (await Create(checkOutDTO.reservation)) ?? 0;
                if (reservationID != 0)
                {
                    foreach (var r in checkOutDTO.reservedTable)
                    {
                        r.ReservationID = reservationID;
                        await _RTS.Create(r);
                    }
                }
            }
            // Send email in background
            BackgroundJob.Enqueue(() => _ESS.SendEmailAsync(
                user.Email,
                "Your Reservation Confirmation",
                $@"
            <div style='font-family: Arial, sans-serif; color: #333;'>
                <h2 style='color: #2c3e50;'>Reservation Confirmed</h2>
                <p>Hello {user.FirstName} {user.LastName},</p>
                <p>Thank you for booking with us. Here are your details:</p>
                <ul>
                    <li><strong>Date:</strong> {checkOutDTO.reservation.DateTime:MMMM dd, yyyy}</li>
                    <li><strong>Secret Key:</strong> <span style='color: #e74c3c; font-weight: bold;'>{checkOutDTO.reservation.SecretKey}</span></li>
                </ul>
                <p>Please show this key when you arrive so we can verify your reservation.</p>
                <p>Best regards,<br/>The Restaurant Team</p>
            </div>",
                _emailSettings
            ));
            return  true;

        }
        private string GenerateSecretKey()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(); // Example: "A1B2C3D4"
        }

    }
}