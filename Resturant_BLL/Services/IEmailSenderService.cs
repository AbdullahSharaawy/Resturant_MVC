using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.Services
{
    public interface IEmailSenderService
    {
       public  Task SendEmailAsync(string source_email,string source_password,string target_email,string subject,String message, string host_email);
    }
}
