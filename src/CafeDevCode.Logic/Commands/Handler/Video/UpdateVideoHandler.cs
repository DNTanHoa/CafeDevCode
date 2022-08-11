using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class UpdateVideoHandler: IRequestHandler<UpdateVideo,
        BaseCommandResultWithData<Video>>
    {
        private readonly AppDatabase database;
        private readonly IMapper mapper;

        public UpdateVideoHandler(AppDatabase database,
            IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }

        public Task<BaseCommandResultWithData<Video>> Handle(
            UpdateVideo request, CancellationToken cancellationToken)
        {
            var result = new BaseCommandResultWithData<Video>();

            try
            {
                var video = database.Videos.FirstOrDefault(x => x.Id == request.Id);

                if (video != null)
                {
                    mapper.Map(request, video);
                    video.SetUpdateInfo(request.UserName ?? string.Empty, AppGlobal.SysDateTime);
                    database.Update(video);
                    database.SaveChanges();

                    result.Success = true;
                }
                else
                {
                    result.Messages = $"Can't not find video with id is {request.Id}";
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
