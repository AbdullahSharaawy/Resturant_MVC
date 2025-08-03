using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;
using Resturant_DAL.ImplementRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.Services
{
    public interface IPaymentService
    {
        public List<PaymentDTO> GetList();
        public PaymentDTO? GetById(int id);
        public Payment? Create(PaymentDTO Payment);
        public Payment? Update(PaymentDTO Payment);
        public bool Delete(int id);
        public int? Create(Payment payment);
        
    }
}
