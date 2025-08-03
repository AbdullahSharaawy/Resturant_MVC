using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resturant_DAL.Entities
{
    public class Reservation
    {
        public Reservation() { }

        public Reservation(int reservationID, int numberOfGuests, string status, DateTime dateTime,
                           int paymentID, DateTime createdOn, string createdBy, int branchID, string userID, decimal cost)
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
            Cost = cost;
        }

        [Key]
        public int ReservationID { get; private set; }

        public int NumberOfGuests { get;  set; }

        public string Status { get; set; }
        public decimal Cost { get; set; }
        public DateTime DateTime { get;  set; }

        [ForeignKey("Payment")]
        public int PaymentID { get; set; }

        public DateTime CreatedOn { get;  set; }

        public string CreatedBy { get;   set; }

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string? DeletedBy { get; set; }

        public bool IsDeleted { get; set; }

        public Payment Payment { get;  set; }

        [ForeignKey("Branch")]
        public int BranchID { get; set; }

        [ForeignKey("User")]
        public string UserID { get;  set; }

        public User User { get;  set; }

        public Branch Branch { get;  set; }

        public List<ReservedTable> ReservedTables { get;  set; }
    }
}

