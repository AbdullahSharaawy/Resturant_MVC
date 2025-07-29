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
        public Table(int tableID, int tableNumber, int capacity, string status, int restaurantID, DateTime createdOn, string createdBy)
        {
            TableID = tableID;
            TableNumber = tableNumber;
            Capacity = capacity;
            Status = status;
            RestaurantID = restaurantID;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
        }

        [Key]
        public int TableID { get; private set; }
        public int TableNumber { get; private set; }
        public int Capacity { get; private set; }
        public string Status { get; private set; }
        [ForeignKey("Resturant")]
        public int RestaurantID { get; private set; }

        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get;  set; }
        public DateTime? DeletedOn { get;  set; }
        public string? DeletedBy { get;  set; }
        public bool IsDeleted { get;  set; }

        public Resturant Restaurant { get;  set; }
        public List<ReservedTable> ReservedTables { get; set; }
    }
}
