using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Resturant_BLL.ImplementServices;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Resturant_BLL.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
       

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
         
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage,EmailSettings emailSettings)
        {
           
            await SendViaSmtp(email, subject, htmlMessage, emailSettings);
        }

        private async Task SendViaSmtp(string email, string subject, string htmlMessage, EmailSettings emailSettings)
        {
            string fromMail = emailSettings.SmtpUser;
            string fromPassword =emailSettings.SmtpPassword; // You must fill this in with an App Password

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(email));
            message.Body = htmlMessage;
            message.IsBodyHtml = true;
            
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = emailSettings.SmtpPort,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = emailSettings.SmtpUseSSL,

            };

            smtpClient.Send(message);
        }
    }
}