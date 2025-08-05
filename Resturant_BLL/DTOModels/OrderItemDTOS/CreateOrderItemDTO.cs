using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_DAL.Entities;

namespace Resturant_BLL.DTOModels.OrderItemDTOs
{
    public class CreateOrderItemDTO
    {
        public int Quantity { get; private set; }
        public MenueItemDTO MenueItem { get; private set; }
    }
}
