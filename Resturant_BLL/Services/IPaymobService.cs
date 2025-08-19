using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.Services
{
    public interface IPaymobService
    {
        public  Task<string> CreatePayment(decimal amount, string currency = "EGP");
    }
}
