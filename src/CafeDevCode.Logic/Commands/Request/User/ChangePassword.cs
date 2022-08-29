using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Request
{
    public class ChangePassword : IIdentifiedCommand,
        IRequest<BaseCommandResult>
    {
        public string? ChangPasswordUserName { get; set; }
        public string? OldPassowrd { get; set; }
        public string? NewPassowrd { get; set; }
        public string? RequestId { get; set; }
        public string? IpAddress { get; set; }
        public string? UserName { get; set; }
    }
}
