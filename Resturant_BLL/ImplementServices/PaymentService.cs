using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Payment? Create(PaymentDTO payment)
        {
            if (payment == null)
                return null;

            Payment mappedPayment = new PaymentMapper().MapToPayment(payment);
            mappedPayment.CreatedOn = DateTime.UtcNow;
            mappedPayment.CreatedBy = "Current User";
            mappedPayment.IsDeleted = false;

            _PR.Create(mappedPayment);
            return mappedPayment;
        }

        public bool Delete(int id)
        {
            Payment p = _PR.GetByID(id);
            if (p == null || p.IsDeleted == true)
            {
                return false;
            }

            p.IsDeleted = true;
            p.DeletedOn = DateTime.UtcNow;
            p.DeletedBy = "Current User";

            _PR.Update(p);
            return true;
        }

        public PaymentDTO? GetById(int id)
        {
            Payment p = _PR.GetByID(id);
            if (p == null || p.IsDeleted == true)
            {
                return null;
            }

            return new PaymentMapper().MapToPaymentDTO(p);
        }

        public List<PaymentDTO> GetList()
        {
            List<Payment> payments = _PR.GetAll().Where(p => p.IsDeleted == false).ToList();

            if (payments == null || payments.Count == 0)
            {
                return new List<PaymentDTO>();
            }

            return new PaymentMapper().MapToPaymentDTOList(payments);
        }

        public Payment? Update(PaymentDTO payment)
        {
            if (payment == null)
                return null;

            Payment mappedPayment = new PaymentMapper().MapToPayment(payment);
            mappedPayment.ModifiedOn = DateTime.UtcNow;
            mappedPayment.ModifiedBy = "Current User";
            mappedPayment.IsDeleted = false;

            _PR.Update(mappedPayment);
            return mappedPayment;
        }
    }
}

