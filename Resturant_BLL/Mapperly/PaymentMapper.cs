using Resturant_BLL.DTOModels.PaymentDTOS;
using Resturant_DAL.Entities;
using Riok.Mapperly.Abstractions;
using System.Collections.Generic;

namespace Resturant_BLL.Mapperly
{
    [Mapper]
    public partial class PaymentMapper
    {
        public partial PaymentDTO MapToPaymentDTO(Payment payment);
        public partial Payment MapToPayment(PaymentDTO paymentDTO);
        public partial List<PaymentDTO> MapToPaymentDTOList(List<Payment> payments);
    }
}