using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels
{
    public class UpdateReservationDTO
    {
        public ReservationDTO ReservationDTO { get; set; }
        public List<BranchDTO> Branches { get; set; }  
 
        public List<ReservationDTO> Reservations { get; set; }
    

}
}
