using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.Entities
{
    public class Table
    {
        [Key]
        public int TableID { get; private set; }
        public int TableNumber { get; private set; }
        public int Capacity { get; private set; }
        public string Status { get; private set; }
        [ForeignKey("Resturant")]
        public int RestaurantID { get; private set; }

        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool IsDeleted { get; private set; }

        public Resturant Restaurant { get; private set; }
        public List<ReservedTable> ReservedTables { get; private set; }
    }
}
