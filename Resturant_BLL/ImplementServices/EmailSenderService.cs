using Castle.Core.Smtp;
using Resturant_BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.ImplementServices
{
    public class EmailSenderService : IEmailSenderService
    {
       public  Task SendEmailAsync(string source_email, string source_password, string target_email, string subject, String message,string host_email) 
        {
           

            using var client = new SmtpClient(host_email, 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(source_email, source_password)
            };

            var mailMessage = new MailMessage(from: source_email, to: target_email, subject, message);
            return client.SendMailAsync(mailMessage);
        }

    }
}
