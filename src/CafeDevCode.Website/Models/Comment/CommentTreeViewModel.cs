using CafeDevCode.Logic.Shared.Models;
using CafeDevCode.Ultils.Model;

namespace CafeDevCode.Website.Models
{
    public class CommentTreeViewModel
    {
        public bool IsRoot { get; set; }
        public IEnumerable<TreeList<CommentModel>> Collection { get; set; } 
            = new List<TreeList<CommentModel>>();
    }
}
