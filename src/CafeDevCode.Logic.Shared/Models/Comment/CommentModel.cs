using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Shared.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public int? PostId { get; set; }
        public int? VideoId { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
    }
}
