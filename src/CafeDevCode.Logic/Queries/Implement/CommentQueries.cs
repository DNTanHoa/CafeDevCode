using CafeDevCode.Database.Entities;
using CafeDevCode.Logic.Queries.Interface;
using CafeDevCode.Ultils.Extensions;
using CafeDevCode.Ultils.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Queries.Implement
{
    public class CommentQueries : ICommentQueries
    {
        private readonly AppDatabase database;
        private readonly IMapper mapper;

        public CommentQueries(AppDatabase database,
            IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }

        public List<CommentModel> GetAll()
        {
            return database.Comments
              .Select(x => mapper.Map<CommentModel>(x))
              .ToList();
        }


        public Task<List<CommentModel>> GetAllAsync()
        {
            return database.Comments
              .Select(x => mapper.Map<CommentModel>(x))
              .ToListAsync();
        }

        public List<CommentModel> GetByPostId(int postId)
        {
            return database.Comments
             .Where(x => x.PostId == postId)
             .Select(x => mapper.Map<CommentModel>(x))
             .ToList();
        }

        public List<CommentModel> GetByVideoId(int videoId)
        {
            return database.Comments
             .Where(x => x.VideoId == videoId)
             .Select(x => mapper.Map<CommentModel>(x))
             .ToList();
        }

        public CommentModel? GetDetail(int id)
        {
            var comment = database.Comments.FirstOrDefault(x => x.Id == id);
            return mapper.Map<CommentModel>(comment);
        }

        public Task<CommentModel?> GetDetailAsync(int id)
        {
            CommentModel? result = null;
            var comment = database.Comments.FirstOrDefault(x => x.Id == id);
            if(comment != null)
            {
                result = mapper.Map<CommentModel>(comment);
            }
            return Task.FromResult(result);
        }

        public BasePagingData<CommentModel> GetPaging(BaseQuery query)
        {
            var comments = database.Comments
                .Where(x => x.Content!.Contains(query.Keywords ?? string.Empty) ||
                            x.IsDeleted != true)
                .Skip(((query.PageIndex - 1) * query.PageSize) ?? 0).Take((query.PageSize * query.PageIndex) ?? 20)
                .Select(x => mapper.Map<CommentModel>(x))
                .ToList();

            var commentCount = database.Authors.Count();

            return new BasePagingData<CommentModel>()
            {
                Items = comments,
                PageSize = query.PageSize ?? 1,
                PageIndex = query.PageIndex ?? 20,
                TotalItem = commentCount,
                TotalPage = (int)Math.Ceiling((double)commentCount / (query.PageSize ?? 20))
            };
        }

        public Task<BasePagingData<CommentModel>> GetPagingAsync(BaseQuery query)
        {
            var comments = database.Comments
                .Where(x => x.Content!.Contains(query.Keywords ?? string.Empty) ||
                            x.IsDeleted != true)
                .Skip(((query.PageIndex - 1) * query.PageSize) ?? 0).Take((query.PageSize * query.PageIndex) ?? 20)
                .Select(x => mapper.Map<CommentModel>(x))
                .ToList();

            var commentCount = database.Authors.Count();

            return Task.FromResult(new BasePagingData<CommentModel>()
            {
                Items = comments,
                PageSize = query.PageSize ?? 1,
                PageIndex = query.PageIndex ?? 20,
                TotalItem = commentCount,
                TotalPage = (int)Math.Ceiling((double)commentCount / (query.PageSize ?? 20))
            });
        }

        public IEnumerable<TreeList<CommentModel>> GetTreeList(int postId, int videoId)
        {
            var data = database.Comments
             .Where(x => (x.VideoId == videoId || x.PostId == postId) &&
                         x.IsDeleted != true)
             .Select(x => mapper.Map<CommentModel>(x));
            var tree = data.GenerateTree(x => x.Id, x => x.ParentId, null);
            return tree;
        }
    }
}
