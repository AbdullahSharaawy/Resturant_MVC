using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;

namespace Resturant_BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> _PR;

        public PaymentService(IRepository<Payment> pr)
        {
            _PR = pr;
        }

        public async Task<Payment?> Create(PaymentDTO payment)
        {
            if (payment == null)
                return null;

            Payment mappedPayment = new PaymentMapper().MapToPayment(payment);
            mappedPayment.CreatedOn = DateTime.UtcNow;
            mappedPayment.CreatedBy = "Current User";
            mappedPayment.IsDeleted = false;

            await _PR.Create(mappedPayment);
            return mappedPayment;
        }

        public async Task<int?> Create(Payment payment)
        {
            return await _PR.Create(payment);
        }

        public async Task<bool> Delete(int id)
        {
            Payment p = await _PR.GetByID(id);
            if (p == null || p.IsDeleted == true)
            {
                return false;
            }

            p.IsDeleted = true;
            p.DeletedOn = DateTime.UtcNow;
            p.DeletedBy = "Current User";

            await _PR.Update(p);
            return true;
        }

        public async Task<PaymentDTO?> GetById(int id)
        {
            Payment p = await _PR.GetByID(id);
            if (p == null || p.IsDeleted == true)
            {
                return null;
            }

            return new PaymentMapper().MapToPaymentDTO(p);
        }

        public async Task<List<PaymentDTO>> GetList()
        {
            List<Payment> payments = (await _PR.GetAll()).Where(p => p.IsDeleted == false).ToList();

            if (payments == null || payments.Count == 0)
            {
                return new List<PaymentDTO>();
            }

            return new PaymentMapper().MapToPaymentDTOList(payments);
        }

        public async Task<Payment?> Update(PaymentDTO payment)
        {
            if (payment == null)
                return null;

            Payment mappedPayment = new PaymentMapper().MapToPayment(payment);
            mappedPayment.ModifiedOn = DateTime.UtcNow;
            mappedPayment.ModifiedBy = "Current User";
            mappedPayment.IsDeleted = false;

            await _PR.Update(mappedPayment);
            return mappedPayment;
        }
    }
}