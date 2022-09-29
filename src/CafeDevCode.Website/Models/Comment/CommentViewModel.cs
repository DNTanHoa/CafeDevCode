using CafeDevCode.Logic.Commands.Request;
using System.ComponentModel.DataAnnotations;

namespace CafeDevCode.Website.Models
{
    public class CommentViewModel : BaseViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nội dung không được để trống")]
        public string Content { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public int? PostId { get; set; }
        public int? VideoId { get; set; }

        public CreateComment ToCreateCommand()
        {
            return new CreateComment()
            {
                Content = this.Content,
                ParentId = this.ParentId,
                PostId = this.PostId,
                VideoId = this.VideoId,
            };
        }

        public UpdateComment ToUpdateCommand()
        {
            return new UpdateComment()
            {
                Id = this.Id,
                Content = this.Content,
                ParentId = this.ParentId,
                PostId = this.PostId,
                VideoId = this.VideoId,
            };
        }
    }
}
