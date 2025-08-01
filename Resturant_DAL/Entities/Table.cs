using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resturant_DAL.Entities
{
    [Table("Table")]
    public class table
    {
        public table() { }

        public table(int tableID, int tableNumber, int capacity, int? branchID)
        {
            TableID = tableID;
            TableNumber = tableNumber;
            Capacity = capacity;
            BranchID = branchID;
        }
       
        [Key]
        public int TableID { get; private set; }
        public int TableNumber { get;  set; }
        public int Capacity { get; set; }
        
         
        [ForeignKey("Branch")]
        public int? BranchID { get;  set; }
        public Branch Branch { get; private set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

        public List<ReservedTable> ReservedTables { get; set; }
    }
}

