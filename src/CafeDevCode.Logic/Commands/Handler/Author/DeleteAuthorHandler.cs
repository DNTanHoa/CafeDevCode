global using AutoMapper;
global using CafeDevCode.Database;
using CafeDevCode.Ultils.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthor, BaseCommandResult>
    {
        private readonly IMapper mapper;
        private readonly AppDatabase database;

        public DeleteAuthorHandler(IMapper mapper,
            AppDatabase database)
        {
            this.mapper = mapper;
            this.database = database;
        }

        public Task<BaseCommandResult> Handle(DeleteAuthor request,
            CancellationToken cancellationToken)
        {
            var result = new BaseCommandResult();

            try
            {
                var author = database.Authors.FirstOrDefault(x => x.Id == request.Id);

                if (author != null)
                {
                    author.MarkAsDelete(request.UserName ?? string.Empty, AppGlobal.SysDateTime);

                    database.Update(author);
                    database.SaveChanges();

                    result.Success = true;
                }
                else
                {
                    result.Messages = $"Can't not find author with id is {request.Id}";
                }
            }
            catch(Exception ex)
            {
                result.Messages = ex.Message;
            }

            return Task.FromResult(result);
        }
    }
}
