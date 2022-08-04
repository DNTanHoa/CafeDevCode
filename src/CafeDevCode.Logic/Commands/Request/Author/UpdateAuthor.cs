global using CafeDevCode.Common.Shared.Interface;
global using CafeDevCode.Common.Shared.Model;
global using CafeDevCode.Database.Entities;
global using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Request
{
    public class UpdateAuthor : Author,
        IIdentifiedCommand,
        IRequest<BaseCommandResultWithData<Author>>
    {
        public string? RequestId { get; set; }
        public string? IpAddress { get; set; }
        public string? UserName { get; set; }
    }
}
