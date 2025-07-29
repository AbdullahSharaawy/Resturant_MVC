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
        public DateTime? ModifiedOn { get; private set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool IsDeleted { get; private set; }

        public EventType EventType { get; private set; }
        public Payment Payment { get; private set; }
       
        public List<ReservedTable> ReservedTables { get; private set; }

    }
}
