using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.ImplementServices
{
    public class EmailSettings
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool SmtpUseSSL { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
       
        public string FromName { get; set; }
    }
}
