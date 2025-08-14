using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels.BranchDTOS
{
    public class BranchDTO
    {
        public int BranchID { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string BuildingNo { get; set; }
        public string StreetName { get; set; }
        public string LocationSelector { get; set; }
    }
}
