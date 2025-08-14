using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels.AdminDTOS
{
    public class AdminDashBoardDTO
    {
        public int TotalReviews { get; set; }
        public int TotalReservations { get; set; }
        public int TotalUsers { get; set; }
    }
}
