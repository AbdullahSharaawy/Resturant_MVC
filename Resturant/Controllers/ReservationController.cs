using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Services;
using Resturant_DAL.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Resturant_PL.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _RS;
        private readonly IBranchService _BR;
        private readonly IReservedTableService _RTS;
        private readonly IPaymentService _PS;
        private readonly UserManager<User> userManager;
        public ReservationController(IReservationService reservationService, IBranchService bR, IReservedTableService rTS, IPaymentService pS, UserManager<User> userManager)
        {
            this._RS = reservationService;
            _BR = bR;
            _RTS = rTS;
            _PS = pS;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var allReservations = await _RS.GetList();
            return View("Reservations", allReservations);
        }

        public async Task<IActionResult> QuickReservationForm()
        {
            return PartialView("_QuickReservation", await _RS.GetCreateReservationInfo());
        }

        public async Task<IActionResult> SaveQuickReservation(UpdateReservationDTO updateReservationDTO)
        {
            CheckOutDTO checkOutDTO = new CheckOutDTO();
           var user= await userManager.GetUserAsync(User);
            updateReservationDTO.ReservationDTO.CreatedBy = user.FirstName+" "+user.LastName;
            var quickReservationResult = await _RS.CreateQuickReservation(updateReservationDTO.ReservationDTO);
            checkOutDTO.reservation = quickReservationResult.Item1;
            checkOutDTO.reservedTable = quickReservationResult.Item2;
            checkOutDTO.Payment = quickReservationResult.Item3;

            if ((checkOutDTO.reservation, checkOutDTO.reservedTable, checkOutDTO.Payment) == (null, null, null))
            {
                updateReservationDTO.Branches = await _BR.GetList();
                return Json(new { success = false, message = "No Seats Available for this Number of Guests." });
            }

            int paymentID = (await _PS.Create(checkOutDTO.Payment)) ?? 0;
            if (paymentID != 0)
            {
                checkOutDTO.reservation.PaymentID = paymentID;
                checkOutDTO.reservation.UserID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                
                int reservationID = (await _RS.Create(checkOutDTO.reservation)) ?? 0;
                if (reservationID != 0)
                {
                    foreach (var r in checkOutDTO.reservedTable)
                    {
                        r.ReservationID = reservationID;
                        await _RTS.Create(r);
                    }
                }
            }

            return Json(new { success = true, message = "Your Reservation is done Successfully" });
        }


        public async Task<IActionResult> MyReservations()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(); // Redirect or show error
            }

            var reservations = await _RS.GetReservationsByUserId(userId);
            var branches = await _BR.GetList();

            var model = new UpdateReservationDTO
            {
                Reservations = reservations,
                Branches = branches
            };

            return PartialView("_MyReservations", model);
        }



        public async Task<IActionResult> Create()
        {
            var model = await _RS.GetCreateReservationInfo();
            return View("Create", model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var reservation = await _RS.GetById(id);
            if (reservation == null)
            {
                TempData["ErrorMessage"] = "Reservation not found.";
                return RedirectToAction("Index");
            }

            var updateModel = new UpdateReservationDTO
            {
                ReservationDTO = reservation,
                Branches = await _BR.GetList()
            };

            return View("Update", updateModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveNew(UpdateReservationDTO dto)
        {
            dto.ReservationDTO.UserID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            dto.ReservationDTO.CreatedOn = DateTime.UtcNow;
            dto.ReservationDTO.CreatedBy = "Current User";

            var result = await _RS.Create(dto.ReservationDTO);
            if (result == null)
            {
                TempData["ErrorMessage"] = "Failed to create reservation.";
                return RedirectToAction("Create");
            }

            TempData["SuccessMessage"] = "Reservation created successfully!";
            return View("Reservations", await _RS.GetList());
        }

        [HttpPost]
        public async Task<IActionResult> SaveEdit(UpdateReservationDTO dto)
        {
            dto.ReservationDTO.ModifiedOn = DateTime.UtcNow;
            dto.ReservationDTO.ModifiedBy = "Current User";

            var result = await _RS.Update(dto.ReservationDTO);
            if (result == null)
            {
                TempData["ErrorMessage"] = "Failed to update reservation.";
                return RedirectToAction("Update", new { id = dto.ReservationDTO.ReservationID });
            }

            TempData["SuccessMessage"] = "Reservation updated successfully!";
            return View("Reservations", await _RS.GetList());
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _RS.Delete(id))
            {
                TempData["SuccessMessage"] = "Reservation deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete the reservation.";
            }

            return RedirectToAction("Index");
        }
    }
}
/*
  using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Resturant_PL.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _RS;
        private readonly IBranchService _BR;
        private readonly IReservedTableService _RTS;
        private readonly IPaymentService _PS;

        public ReservationController(IReservationService reservationService, IBranchService bR, IReservedTableService rTS, IPaymentService pS)
        {
            this._RS = reservationService;
            _BR = bR;
            _RTS = rTS;
            _PS = pS;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> QuickReservationForm()
        {
            return PartialView("_QuickReservation", await _RS.GetCreateReservationInfo());
        }

        public async Task<IActionResult> SaveQuickReservation(UpdateReservationDTO updateReservationDTO)
        {
            CheckOutDTO checkOutDTO = new CheckOutDTO();
            var quickReservationResult = await _RS.CreateQuickReservation(updateReservationDTO.ReservationDTO);
            checkOutDTO.reservation = quickReservationResult.Item1;
            checkOutDTO.reservedTable = quickReservationResult.Item2;
            checkOutDTO.Payment = quickReservationResult.Item3;

            if ((checkOutDTO.reservation, checkOutDTO.reservedTable, checkOutDTO.Payment) == (null, null, null))
            {
                 updateReservationDTO.Branches = await _BR.GetList();
                return Json(new {success=false, message="No Seats Available for this Number of Guests." });
            }

            int paymentID = (await _PS.Create(checkOutDTO.Payment)) ?? 0;
            if (paymentID != 0)
            {
                checkOutDTO.reservation.PaymentID = paymentID;
                checkOutDTO.reservation.UserID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                int reservationID = (await _RS.Create(checkOutDTO.reservation)) ?? 0;
                if (reservationID != 0)
                {
                    foreach (var r in checkOutDTO.reservedTable)
                    {
                        r.ReservationID = reservationID;
                        await _RTS.Create(r);
                    }
                }
            }

            return Json(new { success = true,message="Your Reservation is done Successfuly" });
        }
    }
}
*/