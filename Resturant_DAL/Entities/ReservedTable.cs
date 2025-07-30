using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.Entities
{
    public class ReservedTable
    {
        public ReservedTable() { }
        public ReservedTable(int reservedTableID, DateTime dateTime, int tableID, int reservationID, DateTime createdOn, string createdBy)
        {
            ReservedTableID = reservedTableID;
            DateTime = dateTime;
            TableID = tableID;
            ReservationID = reservationID;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
        }

        [Key]
        public int ReservedTableID { get; private set; }
        public DateTime DateTime { get; private set; }
        [ForeignKey("Table")]
        public int TableID { get; private set; }
        [ForeignKey("Reservation")]
        public int ReservationID { get; private set; }

        public DateTime CreatedOn { get;   set; }
        public string CreatedBy { get;   set; }
        public DateTime? ModifiedOn { get;  set; }
        public string? ModifiedBy { get;  set; }
        public DateTime? DeletedOn { get;  set; }
        public string? DeletedBy { get;  set; }
        public bool IsDeleted { get;  set; }

        public table Table { get;  set; }
        public Reservation Reservation { get;  set; }
         
    }
}
