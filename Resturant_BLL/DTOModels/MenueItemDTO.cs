using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels
{
    public class MenueItemDTO
    {
        public int ItemID { get;  set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public decimal Price { get;  set; }
        public string Category { get;  set; }
        public string? DietaryInfo { get;  set; }
        public bool Availability { get;  set; }
    }
}
