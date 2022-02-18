using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCS.Core.Settings
{
    public class SmtpSettings
    {
        public string Server { get; set; }
        public bool DefaultCredentials { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
