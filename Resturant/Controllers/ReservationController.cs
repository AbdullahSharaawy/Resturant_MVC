using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Services;

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
        public IActionResult QuickReservationForm()
        {
            return View("_QuickReservation", this._RS.GetCreateReservationInfo());
        }
        public IActionResult SaveQuickReservation(UpdateReservationDTO updateReservationDTO)
        {
            CheckOutDTO checkOutDTO = new CheckOutDTO();
            (checkOutDTO.reservation, checkOutDTO.reservedTable,checkOutDTO.Payment) = _RS.CreateQuickReservation(updateReservationDTO.ReservationDTO);

            if ((checkOutDTO.reservation, checkOutDTO.reservedTable, checkOutDTO.Payment) == (null,null,null))
            {
                TempData["SuccessMessage"] = "No Seats available for this number of guests.";
                
                updateReservationDTO.Branches=_BR.GetList();
                return View("_QuickReservation", updateReservationDTO);
            }
            //TempData["CheckOutDTO"] = JsonConvert.SerializeObject(checkOutDTO);
            // the valid loaction for these create operations is completeOrder method in CheckOutController
           
            
            int paymentID= _PS.Create(checkOutDTO.Payment)??0;
            if(paymentID!=0)
            {
                checkOutDTO.reservation.PaymentID = paymentID;
                int reservationID=_RS.Create(checkOutDTO.reservation)??0;
                if (reservationID != 0) 
                {
                    foreach (var r in checkOutDTO.reservedTable)
                    {
                        r.ReservationID = reservationID;
                        _RTS.Create(r);
                    }
                }
                
            }
            return RedirectToAction("Index", "CheckOut");
        }
    }
}
