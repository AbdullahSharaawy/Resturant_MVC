using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels
{
    public class ResturantDTO
    {
        public int RestaurantID { get; set; }

        public string Email { get;  set; }

        public string OperatingHours { get;  set; }

        public string Amenities { get;  set; }
    }
}
