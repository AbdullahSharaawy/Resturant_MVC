using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;

namespace Resturant_BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> _PR;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> userManager;
        public PaymentService(IRepository<Payment> pr, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _PR = pr;
            _httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<Payment?> Create(PaymentDTO payment)
        {
            if (payment == null)
                return null;
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            Payment mappedPayment = new PaymentMapper().MapToPayment(payment);
            mappedPayment.CreatedOn = DateTime.UtcNow;
            mappedPayment.CreatedBy = user.FirstName + " " + user.LastName;
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
           
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            p.IsDeleted = true;
            p.DeletedOn = DateTime.UtcNow;
            p.DeletedBy = user.FirstName+" "+user.LastName;

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
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            Payment mappedPayment =await _PR.GetByID(payment.PaymentID);
            mappedPayment.ModifiedOn = DateTime.UtcNow;
            mappedPayment.ModifiedBy = user.FirstName + " " + user.LastName;
            mappedPayment.IsDeleted = false;
            mappedPayment.Status=payment.Status;
            mappedPayment.Date=payment.Date;
            mappedPayment.Amount=payment.Amount;
            mappedPayment.PaymentMethod = payment.PaymentMethod;
            await _PR.Update(mappedPayment);
            return mappedPayment;
        }
    }
}