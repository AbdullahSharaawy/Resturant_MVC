using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_DAL.Entities;

namespace Resturant_BLL.DTOModels
{
    public class OrderItemDTO
    {
        public int OrderItemID { get; private set; }
        public int Quantity { get; private set; }
        public MenueItemDTO MenueItem { get; private set; }
        public decimal Price => (MenueItem?.Price ?? 0)*Quantity;
    }
}
