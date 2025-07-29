using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.Entities
{
    public class Location
    {
        public Location(int locationID, string city, string area, string buildingNo, string streetName, int? restaurantID, DateTime createdOn, string createdBy)
        {
            LocationID = locationID;
            City = city;
            Area = area;
            BuildingNo = buildingNo;
            StreetName = streetName;
            RestaurantID = restaurantID;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
        }

        [Key]
        public int LocationID { get; private set; }
        public string City { get; private set; }
        public string Area { get; private set; }
        public string BuildingNo { get; private set; }
        public string StreetName { get; private set; }
        [ForeignKey("Resturant")]
        public int? RestaurantID { get; private set; }

        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get;  set; }
        public string? ModifiedBy { get;  set; }
        public DateTime? DeletedOn { get;  set; }
        public string? DeletedBy { get;  set; }
        public bool IsDeleted { get;  set; }

        public Resturant Restaurant { get; private set; }
    }
}
