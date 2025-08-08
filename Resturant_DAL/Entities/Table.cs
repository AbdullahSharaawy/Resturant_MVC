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
        [Required(ErrorMessage ="The Table Number is Required.")]
        [Range(0, int.MaxValue,ErrorMessage ="Please Enter a valid Positive Number.")]
        public int TableNumber { get;  set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please Enter a valid Positive Number.")]
        [Required(ErrorMessage ="The Capacity is Required")]
        public int Capacity { get; set; }
        
         
        [ForeignKey("Branch")]
        public int? BranchID { get;  set; }
        public Branch Branch { get; private set; }
        [Required(ErrorMessage = "Created On is Required.")]
        public DateTime CreatedOn { get; set; }
        [Required(ErrorMessage ="Created By is Required.")]
        public string CreatedBy { get; set; }
        [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])T([01][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])(\.\d{1,7})?Z$", ErrorMessage = "Please Enter A valid Date.")]

        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])T([01][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])(\.\d{1,7})?Z$", ErrorMessage = "Please Enter A valid Date.")]

        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }=false;

        public List<ReservedTable> ReservedTables { get; set; }
    }
}

