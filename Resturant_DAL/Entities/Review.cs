using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.Entities
{
    public class Review
    {
        public Review(int reviewID, string description, DateTime dateTime, int rate, int restaurantID, DateTime createdOn, string createdBy)
        {
            ReviewID = reviewID;
            Description = description;
            DateTime = dateTime;
            Rate = rate;
            RestaurantID = restaurantID;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
        }

        [Key]
        public int ReviewID { get; private set; }
        public string Description { get; private set; }
        public DateTime DateTime { get; private set; }
        public int Rate { get; private set; }
        [ForeignKey("Resturant")]
        public int RestaurantID { get; private set; }
       

        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get;  set; }
        public string? ModifiedBy { get;  set; }
        public DateTime? DeletedOn { get;  set; }
        public string? DeletedBy { get;  set; }
        public bool IsDeleted { get;  set; }

        public Resturant Restaurant { get;  set; }
        
    }
}
