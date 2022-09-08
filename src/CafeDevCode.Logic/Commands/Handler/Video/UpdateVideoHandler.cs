using CafeDevCode.Database.Entities;
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

                    if (request.Tags != null)
                    {
                        var oldVideoTags = database.VideoTags.Where(x => x.VideoId == video.Id);
                        database.RemoveRange(oldVideoTags);

                        var videoTags = request.Tags.Select(x => new VideoTag()
                        {
                            VideoId = video.Id,
                            TagId = x.Id,
                            CreatedAt = AppGlobal.SysDateTime,
                            CreatedBy = request.UserName,
                        });

                        database.VideoTags.AddRange(videoTags);
                    }

                    if(request.PlayLists != null)
                    {
                        var oldPlayListVideos = database.PlayListVideos.Where(x => x.VideoId == video.Id);
                        database.RemoveRange(oldPlayListVideos);

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
