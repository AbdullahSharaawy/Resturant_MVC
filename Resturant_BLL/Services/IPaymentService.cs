using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;

namespace Resturant_BLL.Services
{
    public interface IPaymentService
    {
        public List<PaymentDTO> GetList();
        public PaymentDTO? GetById(int id);
        public Payment? Create(PaymentDTO payment);
        public Payment? Update(PaymentDTO payment);
        public bool Delete(int id);
    }
}
