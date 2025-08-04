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