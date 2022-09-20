using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class CreateCommentHandler
        : IRequestHandler<CreateComment, BaseCommandResultWithData<Comment>>
    {
        private readonly IMapper mapper;
        private readonly AppDatabase database;

        public CreateCommentHandler(IMapper mapper,
            AppDatabase database)
        {
            this.mapper = mapper;
            this.database = database;
        }

        public Task<BaseCommandResultWithData<Comment>> Handle(CreateComment request,
            CancellationToken cancellationToken)
        {
            var result = new BaseCommandResultWithData<Comment>();

            try
            {
                var comment = mapper.Map<Comment>(request);
                comment.SetCreateInfo(request.UserName ?? string.Empty, AppGlobal.SysDateTime);

                database.Comments.Add(comment);
                database.SaveChanges();

                result.Data = comment;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Messages = ex.Message;
            }

            return Task.FromResult(result);
        }
    }
}
