using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Request
{
    public class ResetPassword : IIdentifiedCommand,
        IRequest<BaseCommandResult>
    {
        public string ResetUserName { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string? RequestId { get; set; }
        public string? IpAddress { get; set; }
        public string? UserName { get; set; } = string.Empty;
    }
}
