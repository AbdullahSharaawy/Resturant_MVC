using Resturant_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels
{
    public class CheckOutDTO
    {
        public CheckOutDTO() { }
        public Reservation reservation { get; set; }
        public List<ReservedTable> reservedTable { get; set; }
        public Payment Payment { get; set; }
    }
}
