using CafeDevCode.Logic.Commands.Request;

namespace CafeDevCode.Website.Models
{
    public class CategoryDetailViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? UrlMeta { get; set; } = string.Empty;
        public string? Keywords { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public int? SortIndex { get; set; }

        public CreateCategory ToCreateCommand()
        {
            return new CreateCategory()
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description,
                UrlMeta = this.UrlMeta,
                Keywords = this.Keywords,
                SortIndex = this.SortIndex,
                ParentId = this.ParentId,
            };
        }

        public UpdateCategory ToUpdateCommand()
        {
            return new UpdateCategory()
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description,
                UrlMeta = this.UrlMeta,
                Keywords = this.Keywords,
                SortIndex = this.SortIndex,
                ParentId = this.ParentId,
            };
        }
    }
}
