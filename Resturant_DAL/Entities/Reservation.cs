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
        public Reservation(int reservationID, int numberOfGuests, string status, DateTime dateTime, int paymentID, DateTime createdOn, string createdBy, int branchID, string userID)
        {
            ReservationID = reservationID;
            NumberOfGuests = numberOfGuests;
            Status = status;
            DateTime = dateTime;
            PaymentID = paymentID;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
            BranchID = branchID;
            UserID = userID;
        }

        [Key]
        public int ReservationID { get; private set; }
        public int NumberOfGuests { get; private set; }
        public string Status { get; private set; }
        public DateTime DateTime { get; private set; }
        [ForeignKey("Payment")]
        public int PaymentID { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get;  set; }
        public string? ModifiedBy { get;  set; }
        public DateTime? DeletedOn { get;  set; }
        public string? DeletedBy { get;  set; }
        public bool IsDeleted { get;  set; }
        public Payment Payment { get; private set; }
        [ForeignKey("Branch")]
        public int BranchID { get; private set; }
        [ForeignKey("User")]
        public string UserID { get; private set; } 
        public User User { get; private set; }
        public Branch Branch { get; private set; }
        public List<ReservedTable> ReservedTables { get; private set; }

    }
}
