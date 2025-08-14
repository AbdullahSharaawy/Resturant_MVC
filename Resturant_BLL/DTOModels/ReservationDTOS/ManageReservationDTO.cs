using Resturant_BLL.DTOModels.BranchDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels.ReservationDTOS
{
    public class ManageReservationDTO
    {
        public ReservationDTO ReservationDTO { get; set; }
        public List<BranchDTO> Branches { get; set; }

        public List<ReservationDTO> Reservations { get; set; }


    }
}
