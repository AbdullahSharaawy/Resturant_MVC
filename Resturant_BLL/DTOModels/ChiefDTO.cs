using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels
{
    public class ChiefDTO
    {
        public int ChiefID { get;  set; }
        public string Name { get;  set; }
        public string PhoneNumber { get;  set; }
        public string Email { get; set; }
        public string Position { get;  set; }
        public string? City { get; set; }
        public int BranchID { get; set; }
    }
}
