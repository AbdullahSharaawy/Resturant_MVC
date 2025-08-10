using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.Entities
{
    public class Branch   
    {
        public Branch() { }
        public Branch(int branchID, string city, string area, string buildingNo, string streetName, DateTime createdOn, string createdBy)
        {
            BranchID = branchID;
            City = city;
            Area = area;
            BuildingNo = buildingNo;
            StreetName = streetName;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
        }
        
        [Key]
        public int BranchID { get; private set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get;  set; }
        [Required(ErrorMessage = "Area is required")]
        public string Area { get;  set; }
        [Required(ErrorMessage = "Building number is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Building number must be greater than or equal zero")]
        [RegularExpression(@"^-?\d+$",ErrorMessage = "Building number must be greater than or equal zero.")]
        public string BuildingNo { get; set; }
        [Required(ErrorMessage = "Location Selector is required")]
        public string LocationSelector { get; set; }// value of google maps
        [Required(ErrorMessage = "Street name is required")]
        public string StreetName { get;  set; }
        [Required(ErrorMessage = "Creation date is required")]
        
        public DateTime CreatedOn { get;  set; }

        [Required(ErrorMessage = "Creator name is required")]
        public string CreatedBy { get;  set; }
        [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])T([01][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])(\.\d{1,7})?Z$",ErrorMessage ="Please Enter A valid Date.")]
        public DateTime? ModifiedOn { get;  set; }
        public string? ModifiedBy { get;  set; }
        [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])T([01][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])(\.\d{1,7})?Z$", ErrorMessage = "Please Enter A valid Date.")]

        public DateTime? DeletedOn { get;  set; }
        public string? DeletedBy { get;  set; }
        public bool IsDeleted { get;  set; }= false;
        
    }
}
