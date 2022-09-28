using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class DeleteCommentHandler : IRequestHandler<DeleteComment, BaseCommandResult>
    {
        private readonly IMapper mapper;
        private readonly AppDatabase database;

        public DeleteCommentHandler(IMapper mapper,
            AppDatabase database)
        {
            this.mapper = mapper;
            this.database = database;
        }

        public Task<BaseCommandResult> Handle(DeleteComment request, 
            CancellationToken cancellationToken)
        {
            var result = new BaseCommandResult();

            try
            {
                var comment = database.Comments.FirstOrDefault(x => x.Id == request.Id);

                if (comment != null)
                {
                    comment.MarkAsDelete(request.UserName ?? string.Empty, AppGlobal.SysDateTime);

                    database.Update(comment);
                    database.SaveChanges();

                    result.Success = true;
                }
                else
                {
                    result.Messages = $"Can't not find category with id is {request.Id}";
                }
            }
            catch (Exception ex)
            {
                result.Messages = ex.Message;
            }

            return Task.FromResult(result);
        }
    }
}
