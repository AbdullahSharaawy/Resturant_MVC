using Resturant_DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels
{
   public class ReservationDTO
    {
        public int ReservationID { get; set; }  
        public int NumberOfGuests { get;  set; }

        public string Status { get;  set; }

        public DateTime DateTime { get; set; }

        public string PaymentMethod { get;  set; }
        public int PaymentID { get;  set; }
        public string City { get; set; }
        public int BranchID { get;  set; }

        public string? UserID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }

    }
}
