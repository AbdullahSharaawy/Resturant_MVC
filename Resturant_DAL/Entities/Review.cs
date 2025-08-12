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
        public Review() { }
        public Review(int reviewID, string description, DateTime dateTime, int rate, DateTime createdOn, string createdBy, string userID)
        {
            ReviewID = reviewID;
            Description = description;
            DateTime = dateTime;
            Rate = rate;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
            UserID = userID;
        }

        [Key]
        public int ReviewID { get; private set; }
        public string Description { get;  set; }
        public DateTime DateTime { get; set; }
        public int Rate { get;  set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get;  set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("User")]
        public string UserID { get; set; }

        public User User { get; set; }

    }
}
