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
        public int ReservedTableID { get;  set; }
        public DateTime DateTime { get;  set; }
        
        public int TableID { get; set; }
        public int ReservationID { get;  set; } 

       
       
    }
}
