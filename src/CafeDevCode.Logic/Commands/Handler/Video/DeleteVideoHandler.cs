using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class DeleteVideoHandler 
        : IRequestHandler<DeleteVideo, BaseCommandResult>
    {
        private readonly IMapper mapper;
        private readonly AppDatabase database;

        public DeleteVideoHandler(IMapper mapper,
            AppDatabase database)
        {
            this.mapper = mapper;
            this.database = database;
        }

        public Task<BaseCommandResult> Handle(DeleteVideo request,
            CancellationToken cancellationToken)
        {
            var result = new BaseCommandResult();

            try
            {
                var video = database.Videos.FirstOrDefault(x => x.Id == request.Id);

                if (video != null)
                {
                    video.MarkAsDelete(request.UserName ?? string.Empty, AppGlobal.SysDateTime);

                    database.Update(video);
                    database.SaveChanges();

                    result.Success = true;
                }
                else
                {
                    result.Messages = $"Can't not find play list with id is {request.Id}";
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
