using System.Collections.Generic;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;

namespace Resturant_BLL.Services
{
    public interface IPaymentService
    {
        public Task<List<PaymentDTO>> GetList();
        public Task<PaymentDTO?> GetById(int id);
        public Task<Payment?> Create(PaymentDTO Payment);
        public Task<Payment?> Update(PaymentDTO Payment);
        public Task<bool> Delete(int id);
        public Task<int?> Create(Payment payment);
    }
}