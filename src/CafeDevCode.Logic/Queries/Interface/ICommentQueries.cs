using CafeDevCode.Ultils.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Queries.Interface
{
    public interface ICommentQueries
    {
        BasePagingData<CommentModel> GetPaging(BaseQuery query);
        List<CommentModel> GetAll();
        List<CommentModel> GetByPostId(int postId);
        List<CommentModel> GetByVideoId(int videoId);
        IEnumerable<TreeList<CommentModel>> GetTreeList(int postId, int videoId);
        CommentModel? GetDetail(int id);
        Task<CommentModel?> GetDetailAsync(int id);
        Task<BasePagingData<CommentModel>> GetPagingAsync(BaseQuery query);
        Task<List<CommentModel>> GetAllAsync();
    }
}
