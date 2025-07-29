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
        [Key]
        public int ReservedTableID { get; private set; }
        public DateTime DateTime { get; private set; }
        [ForeignKey("Table")]
        public int TableID { get; private set; }
        [ForeignKey("Reservation")]
        public int ReservationID { get; private set; }

        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool IsDeleted { get; private set; }

        public Table Table { get; private set; }
        public Reservation Reservation { get; private set; }
    }
}
