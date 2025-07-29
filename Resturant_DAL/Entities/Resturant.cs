using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.Entities
{
    public class Resturant
    {
        public Resturant(int restaurantID, string email, string operatingHours, string amenities)
        {
            RestaurantID = restaurantID;
            Email = email;
            OperatingHours = operatingHours;
            Amenities = amenities;
           
        }

        [Key]
        public int RestaurantID { get; private set; }
       
        public string Email { get; private set; }
        
        public string OperatingHours { get; private set; }
        
        public string Amenities { get; private set; }

        public DateTime CreatedOn { get;  set; }
        public string CreatedBy { get;  set; }
        public DateTime? ModifiedOn { get;  set; }
        public string? ModifiedBy { get;  set; }
        public DateTime? DeletedOn { get;  set; }
        public string? DeletedBy { get;  set; }
        public bool IsDeleted { get;  set; }

    }
}
