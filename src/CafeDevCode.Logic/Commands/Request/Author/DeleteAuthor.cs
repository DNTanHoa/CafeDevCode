using CafeDevCode.Common.Shared.Interface;
using CafeDevCode.Common.Shared.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Request
{
    public class DeleteAuthor :
        IIdentifiedCommand,
        IRequest<BaseCommandResult>
    {
        public int Id { get; set; }
        public string? RequestId { get; set; }
        public string? IpAddress { get; set; }
        public string? UserName { get; set; }
    }
}
