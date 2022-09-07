using CafeDevCode.Database.Entities;
using CafeDevCode.Logic.Commands.Request;

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
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<PlayList> PlayLists { get; set; } = new List<PlayList>();
        public CreateVideo ToCreateCommand()
        {
            return new CreateVideo
            {
                Title = this.Title,
                Summary = this.Summary,
                Description = this.Description,
                Url = this.Url,
                UrlMeta = this.UrlMeta,
                Iframe = this.Iframe,
                Keywords = this.Keywords,
                PostDate = this.PostDate,
                Remark = this.Remark,
                Tags = this.Tags,
                PlayLists = this.PlayLists,
            };
        }

        public UpdateVideo ToUpdateCommand()
        {
            return new UpdateVideo
            {
                Id = this.Id,
                Title = this.Title,
                Summary = this.Summary,
                Description = this.Description,
                Url = this.Url,
                UrlMeta = this.UrlMeta,
                Iframe = this.Iframe,
                Keywords = this.Keywords,
                PostDate = this.PostDate,
                Remark = this.Remark,
                Tags = this.Tags,
                PlayLists = this.PlayLists,
            };
        }
    }
}
