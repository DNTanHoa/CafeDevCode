using CafeDevCode.Logic.Commands.Request;
using CafeDevCode.Website.Models;

namespace CafeDevCode.Website.Model
{
    public class TagDetailViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? UrlMeta { get; set; } = string.Empty;

        public CreateTag ToCreateCommand()
        {
            return new CreateTag()
            {
                Id = Id,
                Title = Title,
                Description = Description,
                UrlMeta = UrlMeta,
            };
        }

        public UpdateTag ToUpdateCommand()
        {
            return new UpdateTag()
            {
                Id = Id,
                Title = Title,
                Description = Description,
                UrlMeta = UrlMeta,
            };
        }
    }
}
