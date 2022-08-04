using CafeDevCode.Common.Shared.Interface;
using CafeDevCode.Common.Shared.Model;
using CafeDevCode.Database.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Request
{
    public class CreateAuthor : Author,
        IIdentifiedCommand,
        IRequest<BaseCommandResultWithData<Author>>
    {
        public string? RequestId { get; set; }
        public string? IpAddress { get; set; }
        public string? UserName { get; set; }
    }
}
