using CafeDevCode.Logic.Commands.Request;
using System.ComponentModel.DataAnnotations;

namespace CafeDevCode.Website.Models
{
    public class PlayListViewModel : BaseViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? UrlMeta { get; set; }
        public string? Keywords { get; set; }
        public int? SortIndex { get; set; }

        public CreatePlayList ToCreateCommand()
        {
            return new CreatePlayList
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description,
                Keywords = this.Keywords,
                SortIndex = this.SortIndex,
                UrlMeta = this.UrlMeta,
            };
        }

        public UpdatePlayList ToUpdateCommand()
        {
            return new UpdatePlayList
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description,
                Keywords = this.Keywords,
                SortIndex = this.SortIndex,
                UrlMeta = this.UrlMeta
            };
        }
    }
}
