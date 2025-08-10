using Resturant_BLL.DTOModels.OrderDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels.OrderDTOS
{
    public class MyOrdersDTO
    {
        
        public List<ReadOrderDTO> Orders { get; set; }

        //public static implicit operator MyOrdersDTO(MyOrdersDTO v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
