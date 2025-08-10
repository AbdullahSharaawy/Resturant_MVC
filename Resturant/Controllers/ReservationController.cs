using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant_BLL.DTOModels;
using Resturant_BLL.DTOModels.ReservationDTOS;


using Resturant_BLL.Services;
using Resturant_DAL.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Resturant_PL.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _RS;
        private readonly IBranchService _BS;
        private readonly IReservedTableService _RTS;
        private readonly IPaymentService _PS;
        private readonly UserManager<User> userManager;
        public ReservationController(IReservationService reservationService, IBranchService bR, IReservedTableService rTS, IPaymentService pS, UserManager<User> userManager)
        {
            this._RS = reservationService;
            _BS = bR;
            _RTS = rTS;
            _PS = pS;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var allReservations = await _RS.GetList();
            return View("Index", allReservations);
        }

        public async Task<IActionResult> QuickReservationForm()
        {
            return PartialView("_QuickReservation", await _RS.GetCreateReservationInfo());
        }

        public async Task<IActionResult> SaveQuickReservation(UpdateReservationDTO updateReservationDTO)
        {
            
            if(await _RS.FinishQuickReservation(updateReservationDTO))
                return Json(new { success = true, message = "Your Reservation is done Successfully" });
            return Json(new { success = false, message = "No Seats Available for this Number of Guests." });
        }


        public async Task<IActionResult> MyReservations()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(); // Redirect or show error
            }

            var reservations = await _RS.GetReservationsByUserId(userId);
            var branches = await _BS.GetList();

            var model = new UpdateReservationDTO
            {
                Reservations = reservations,
                Branches = branches
            };

            return PartialView("_MyReservations", model);
        }



        public async Task<IActionResult> Create()
        {
            UpdateReservationDTO model = await _RS.GetCreateReservationInfo();
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
                Branches = await _BS.GetList()
            };

            return View("Update", updateModel);
        }

       

        [HttpPost]
        public async Task<IActionResult> SaveEdit(UpdateReservationDTO dto)
        {

            if (await _RS.Update(dto.ReservationDTO)!=null)
                return Json(new { success = true, message = "Your Update is done Successfully" });
            return Json(new { success = false, message = "No Seats Available for this Number of Guests." });

        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _RS.Delete(id))
            {
                TempData["SuccessMessage"] = "Record deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete the record.";
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteMyReservation(int id)
        {
            if (await _RS.Delete(id))
                return Json(new { success = true, redirectUrl = Url.Action("Index", "UserProfile") });
            return Json(new { success = false, message = "Failed to delete the record." });

        }
        public async Task<IActionResult> PaymentCompleted(int id)
        {
            var reservation = await _RS.GetById(id);
            var payment = await _PS.GetById(reservation.PaymentID);
            payment.Status = "Completed";
            
            if(await _PS.Update(payment)!=null)
                TempData["SuccessMessage"] = "Record deleted successfully.";
            else
                TempData["ErrorMessage"] = "Failed to delete the record.";
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
        private readonly IBranchService _BS;
        private readonly IReservedTableService _RTS;
        private readonly IPaymentService _PS;

        public ReservationController(IReservationService reservationService, IBranchService bR, IReservedTableService rTS, IPaymentService pS)
        {
            this._RS = reservationService;
            _BS = bR;
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
                 updateReservationDTO.Branches = await _BS.GetList();
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