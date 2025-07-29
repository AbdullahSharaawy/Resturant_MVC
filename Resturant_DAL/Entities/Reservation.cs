using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.Entities
{
    public class Reservation
    {
        public Reservation(int reservationID, int numberOfGuests, string status, DateTime dateTime, int eventTypeID, int paymentID, DateTime createdOn, string createdBy)
        {
            ReservationID = reservationID;
            NumberOfGuests = numberOfGuests;
            Status = status;
            DateTime = dateTime;
            EventTypeID = eventTypeID;
            PaymentID = paymentID;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
        }

        [Key]
        public int ReservationID { get; private set; }
        public int NumberOfGuests { get; private set; }
        public string Status { get; private set; }
        public DateTime DateTime { get; private set; }
        [ForeignKey("EventType")]
        public int EventTypeID { get; private set; }
        [ForeignKey("Payment")]
        public int PaymentID { get; private set; }
        
        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get;  set; }
        public string? ModifiedBy { get;  set; }
        public DateTime? DeletedOn { get;  set; }
        public string? DeletedBy { get;  set; }
        public bool IsDeleted { get;  set; }

        public EventType EventType { get; private set; }
        public Payment Payment { get; private set; }
       
        public List<ReservedTable> ReservedTables { get; private set; }

    }
}
