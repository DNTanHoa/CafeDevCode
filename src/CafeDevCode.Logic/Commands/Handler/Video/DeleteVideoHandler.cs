using CafeDevCode.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

                    var videoTags = database.VideoTags.Where(x => x.VideoId == video.Id).ToList();
                    videoTags.ForEach(x =>
                    {
                        x.MarkAsDelete(request.UserName ?? String.Empty, AppGlobal.SysDateTime);
                    });
                    database.VideoTags.UpdateRange(videoTags);

                    var playListVideos = database.PlayListVideos.Where(x => x.VideoId == video.Id).ToList();
                    playListVideos.ForEach(x =>
                    {
                        x.MarkAsDelete(request.UserName ?? String.Empty, AppGlobal.SysDateTime);
                    });
                    database.PlayListVideos.UpdateRange(playListVideos);

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
