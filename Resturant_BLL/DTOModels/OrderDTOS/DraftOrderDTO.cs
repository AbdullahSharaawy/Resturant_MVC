using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels.OrderItemDTOs;
using Resturant_DAL.Entities;

namespace Resturant_BLL.DTOModels.OrderDTOs
{
    public class DraftOrderDTO
    {
        public int OrderID { get; set; }
        public List<ReadOrderItemDTO> OrderItems { get; set; } = new();
    }
}
