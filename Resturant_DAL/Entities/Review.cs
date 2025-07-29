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
        [Key]
        public int ReviewID { get; private set; }
        public string Description { get; private set; }
        public DateTime DateTime { get; private set; }
        public int Rate { get; private set; }
        [ForeignKey("Resturant")]
        public int RestaurantID { get; private set; }
       

        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool IsDeleted { get; private set; }

        public Resturant? Restaurant { get; private set; }
        
    }
}
