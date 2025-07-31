using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels
{
    public class ReviewDTO
    {
        public int ReviewID { get; set; }
        public int CustomerID { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
    }
}
