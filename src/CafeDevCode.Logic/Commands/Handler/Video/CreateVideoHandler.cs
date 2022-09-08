using CafeDevCode.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class CreateVideoHandler 
        : IRequestHandler<CreateVideo, BaseCommandResultWithData<Video>>
    {
        private readonly IMapper mapper;
        private readonly AppDatabase database;

        public CreateVideoHandler(IMapper mapper,
            AppDatabase database)
        {
            this.mapper = mapper;
            this.database = database;
        }

        public Task<BaseCommandResultWithData<Video>> Handle(CreateVideo request,
            CancellationToken cancellationToken)
        {
            var result = new BaseCommandResultWithData<Video>();

            try
            {
                var video = mapper.Map<Video>(request);
                database.Videos.Add(video);

                video.SetCreateInfo(request.UserName ?? string.Empty, AppGlobal.SysDateTime);
                database.SaveChanges();

                if (request.Tags != null)
                {
                    var videoTags = request.Tags.Select(x => new VideoTag()
                    {
                        VideoId = video.Id,
                        TagId = x.Id,
                        CreatedAt = AppGlobal.SysDateTime,
                        CreatedBy = request.UserName,
                    });

                    database.VideoTags.AddRange(videoTags);
                }

                if (request.PlayLists != null)
                {
                    var playListVideos = request.PlayLists.Select(x => new PlayListVideo()
                    {
                        VideoId = video.Id,
                        PlayListId = x.Id,
                        CreatedAt = AppGlobal.SysDateTime,
                        CreatedBy = request.UserName,
                    });

                    database.PlayListVideos.AddRange(playListVideos);
                }

                database.SaveChanges();

                result.Success = true;
                result.Data = video;
            }
            catch (Exception ex)
            {
                result.Messages = ex.Message;
            }

            return Task.FromResult(result);
        }
    }
}
