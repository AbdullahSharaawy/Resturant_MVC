using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.Entities
{
    public class Payment
    {
        public Payment() { }
        public Payment(int paymentID, string paymentMethod, decimal amount, DateTime date, string status, DateTime createdOn, string createdBy)
        {
            PaymentID = paymentID;
            PaymentMethod = paymentMethod;
            Amount = amount;
            Date = date;
            Status = status;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
        }

        [Key]
        public int PaymentID { get; private set; }
        public string PaymentMethod { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public string Status { get; private set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get;  set; }
        public string? ModifiedBy { get;  set; }
        public DateTime? DeletedOn { get;  set; }
        public string? DeletedBy { get;  set; }
        public bool IsDeleted { get;  set; }
    }
}
