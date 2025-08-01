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
        public string City { get;  set; }
        public string Area { get;  set; }
        public string BuildingNo { get; set; }
        public string StreetName { get;  set; }
        public DateTime CreatedOn { get;  set; }
        public string CreatedBy { get;  set; }
        public DateTime? ModifiedOn { get;  set; }
        public string? ModifiedBy { get;  set; }
        public DateTime? DeletedOn { get;  set; }
        public string? DeletedBy { get;  set; }
        public bool IsDeleted { get;  set; }
        
    }
}
