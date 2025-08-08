using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels.OrderItemDTOs
{
    public class UpdateOrderItemDTO
    {
        public int OrderItemID { get; set; }
        public int Quantity { get; set; }
        public MenueItemDTO MenueItem { get; set; }
        public string ItemName { get; set; }
    }
}
