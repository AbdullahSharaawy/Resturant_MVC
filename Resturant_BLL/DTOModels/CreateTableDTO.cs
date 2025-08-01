using Resturant_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels
{
    public class CreateTableDTO
    {
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public string City { get; set; }
        public int BranchID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Branch Branch { get; set; }
       public List<BranchDTO> branchDTOs { get; set; }
    }
}
