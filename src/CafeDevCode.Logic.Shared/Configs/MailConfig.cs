using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Shared.Configs
{
    public class MailConfig
    {
        public static string ConfigName { get; set; } = "MailConfig";
        public string Host { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Port { get; set; }
        public string DefaultToMailAddress { get; set; } = string.Empty;
    }
}
