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
        public DateTime? ModifiedOn { get; private set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool IsDeleted { get; private set; }

        public Resturant Restaurant { get; private set; }
    }
}
