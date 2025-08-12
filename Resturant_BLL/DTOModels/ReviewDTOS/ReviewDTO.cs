using Resturant_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels.ReviewDTOS
{
    public class ReviewDTO
    {
        public int ReviewID { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Rate { get; set; }
        public string? ImagePath { get; set; } = "PersonIcon.svg";
    }
}
