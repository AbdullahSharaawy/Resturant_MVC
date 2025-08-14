using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels.PaymentDTOS
{
    public class PaymentDTO
    {
        public int PaymentID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
    }
}
