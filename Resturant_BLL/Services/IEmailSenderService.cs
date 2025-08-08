using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Resturant_BLL.ImplementServices;

namespace Resturant_BLL.Services
{
    public interface IEmailSenderService
    {
        public  Task SendEmailAsync(string email, string subject, string htmlMessage, EmailSettings emailSettings);
       
    
    }
}