using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels
{
    public class TableDTO
    {
        public TableDTO() { }
        public int TableID { get;  set; }
        public int TableNumber { get;  set; }
        public int Capacity { get;  set; }
        public string City {  get; set; }
        public int BranchID { get; set; }
        
    }
}
