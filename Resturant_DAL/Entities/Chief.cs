using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.Entities
{
    public class Chief
    {
        public Chief() { }
        public Chief(int chiefID, string name, string phoneNumber, string email, string position, int? branchID)
        {
            ChiefID = chiefID;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Position = position;
            BranchID = branchID;
        }

        [Key]
        public int ChiefID { get; private set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get;  set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^\+?[0-9\s\-()]{7,20}$", ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",ErrorMessage ="Please Enter a valid Email.")]
        public string Email { get;  set; }
        [Required(ErrorMessage = "Position is required")]
        public string Position { get;  set; }

        [ForeignKey("Branch")] 
        public int? BranchID { get;  set; }
        [Required(ErrorMessage = "Created on is required")]
        public DateTime CreatedOn { get;  set; }
        [Required(ErrorMessage = "Created by is required")]
        public string CreatedBy { get;  set; }
        [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])T([01][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])(\.\d{1,7})?Z$", ErrorMessage = "Please Enter A valid Date.")]

        public DateTime? ModifiedOn { get;  set; }
        public string? ModifiedBy { get;  set; }
        [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])T([01][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])(\.\d{1,7})?Z$", ErrorMessage = "Please Enter A valid Date.")]

        public DateTime? DeletedOn { get;  set; }
        public string? DeletedBy { get;  set; }
        public bool IsDeleted { get;  set; }=false;
        public  Branch Branch { get;  set; }
    }
}