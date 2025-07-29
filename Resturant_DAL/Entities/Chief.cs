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
        public Chief(int chiefID, string name, string phoneNumber, string email, string position, int? restaurantID, DateTime createdOn, string createdBy)
        {
            ChiefID = chiefID;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Position = position;
            RestaurantID = restaurantID;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
        }

        [Key]
        public int ChiefID { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string Position { get; private set; }
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
