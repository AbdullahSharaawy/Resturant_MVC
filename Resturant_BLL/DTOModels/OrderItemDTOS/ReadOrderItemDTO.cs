using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels.OrderItemDTOs
{
    public class ReadOrderItemDTO
    {
        public MenueItemDTO MenueItem { get; private set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
