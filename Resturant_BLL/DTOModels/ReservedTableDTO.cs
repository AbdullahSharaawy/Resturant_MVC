using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels
{
   public  class ReservedTableDTO
    {
        public ReservedTableDTO() { }
        public int ReservedTableID { get; private set; }
        public DateTime DateTime { get; private set; }
        [ForeignKey("Table")]
        public int TableID { get; private set; }
        [ForeignKey("Reservation")]
        public int ReservationID { get; private set; } = 0;

        public int Capacity { get; set; }
        public int TableId { get; set; }
    }
}
