using Resturant_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels
{
   public  class ReservationDTO
    {
        public ReservationDTO() { }
        public int ReservationID { get; private set; }
        public int NumberOfGuests { get; private set; }
        public string Status { get; private set; }
        public Payment Payment { get; private set; }

    }
}
