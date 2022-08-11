using CafeDevCode.Logic.Commands.Request.Video;

namespace CafeDevCode.Website.Models
{
    public class VideoDetailViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Summary { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? UrlMeta { get; set; } = string.Empty;
        public string? Keywords { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
        public string? Url { get; set; } = string.Empty;
        public string? Iframe { get; set; } = string.Empty;
        public DateTime? PostDate { get; set; }
        public string? Remark { get; set; } = string.Empty;

        public CreateVideo ToCreateCommand()
        {
            return new CreateVideo
            {
            };
        }

        public UpdateVideo ToUpdateCommand()
        {
            return new UpdateVideo
            {

            };
        }
    }
}
