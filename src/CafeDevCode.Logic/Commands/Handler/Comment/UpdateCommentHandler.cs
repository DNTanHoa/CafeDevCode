using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class UpdateCommentHandler : IRequestHandler<UpdateComment,
        BaseCommandResultWithData<Comment>>
    {
        private readonly AppDatabase database;
        private readonly IMapper mapper;

        public UpdateCommentHandler(AppDatabase database,
            IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }

        public Task<BaseCommandResultWithData<Comment>> Handle(
            UpdateComment request, CancellationToken cancellationToken)
        {
            var result = new BaseCommandResultWithData<Comment>();

            try
            {
                var comment = database.Comments.FirstOrDefault(x => x.Id == request.Id);

                if (comment != null)
                {
                    mapper.Map(request, comment);
                    comment.SetUpdateInfo(request.UserName ?? string.Empty, AppGlobal.SysDateTime);
                    database.Update(comment);
                    database.SaveChanges();

                    result.Success = true;
                    result.Data = comment;
                }
                else
                {
                    result.Messages = $"Can't not find comment with id is {request.Id}";
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
