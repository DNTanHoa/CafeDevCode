using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class DeleteTagHandler 
        : IRequestHandler<DeleteTag, BaseCommandResult>
    {
        private readonly IMapper mapper;
        private readonly AppDatabase database;

        public DeleteTagHandler(IMapper mapper,
            AppDatabase database)
        {
            this.mapper = mapper;
            this.database = database;
        }

        public Task<BaseCommandResult> Handle(DeleteTag request,
            CancellationToken cancellationToken)
        {
            var result = new BaseCommandResult();

            try
            {
                var tag = database.Tags.FirstOrDefault(x => x.Id == request.Id);

                if (tag != null)
                {
                    tag.MarkAsDelete(request.UserName ?? string.Empty, AppGlobal.SysDateTime);

                    database.Update(tag);
                    database.SaveChanges();

                    result.Success = true;
                }
                else
                {
                    result.Messages = $"Can't not find tag with id is {request.Id}";
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
