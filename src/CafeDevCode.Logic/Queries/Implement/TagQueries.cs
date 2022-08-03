using AutoMapper;
using CafeDevCode.Common.Shared.Model;
using CafeDevCode.Database;
using CafeDevCode.Logic.Queries.Interface;
using CafeDevCode.Logic.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Queries.Implement
{
    public class TagQueries : ITagQueries
    {
        private readonly AppDatabase database;
        private readonly IMapper mapper;

        public TagQueries(AppDatabase database,
            IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }
        public List<TagSummaryModel> GetAll()
        {
            return database.Tags
              .Select(x => mapper.Map<TagSummaryModel>(x))
              .ToList();
        }

        public Task<List<TagSummaryModel>> GetAllAsync()
        {
            return database.Tags
              .Select(x => mapper.Map<TagSummaryModel>(x))
              .ToListAsync();
        }

        public TagDetailModel? GetDetail(int id)
        {
            var tag = database.Tags.FirstOrDefault(x => x.Id == id);

            if (tag != null)
            {
                var result = mapper.Map<TagDetailModel>(tag);

                var tagPostIds = database.PostCategories.Where(x => x.CategoryId == id)
                    .Select(x => x.PostId);

                var posts = database.Post.Where(x => tagPostIds.Contains(x.Id));

                result.Posts = posts.ToList();
                return result;
            }

            return null;
        }

        public Task<TagDetailModel?> GetDetailAsync(int id)
        {
            TagDetailModel? result = null;
            var tag = database.Tags.FirstOrDefault(x => x.Id == id);

            if (tag != null)
            {
                result = mapper.Map<TagDetailModel>(tag);

                var tagPostIds = database.PostCategories.Where(x => x.CategoryId == id)
                    .Select(x => x.PostId);

                var posts = database.Post.Where(x => tagPostIds.Contains(x.Id));

                result.Posts = posts.ToList();
            }

            return Task.FromResult(result);
        }

        public BasePagingData<TagSummaryModel> GetPaging(BaseQuery query)
        {
            var tags = database.Tags
                .Where(x => x.Title!.Contains(query.Keywords ?? string.Empty) ||
                            x.Description!.Contains(query.Keywords ?? string.Empty) ||
                            x.IsDeleted != true)
                .Skip(((query.PageIndex - 1) * query.PageSize) ?? 0).Take((query.PageSize * query.PageIndex) ?? 20)
                .Select(x => mapper.Map<TagSummaryModel>(x))
                .ToList();

            var categoryCount = database.Authors.Count();

            return new BasePagingData<TagSummaryModel>()
            {
                Items = tags,
                PageSize = query.PageSize ?? 1,
                PageIndex = query.PageIndex ?? 20,
                TotalItem = categoryCount,
                TotalPage = (int)Math.Ceiling((double)categoryCount / (query.PageSize ?? 20))
            };
        }

        public Task<BasePagingData<TagSummaryModel>> GetPagingAsync(BaseQuery query)
        {
            var tags = database.Tags
                .Where(x => x.Title!.Contains(query.Keywords ?? string.Empty) ||
                            x.Description!.Contains(query.Keywords ?? string.Empty) ||
                            x.IsDeleted != true)
                .Skip(((query.PageIndex - 1) * query.PageSize) ?? 0).Take((query.PageSize * query.PageIndex) ?? 20)
                .Select(x => mapper.Map<TagSummaryModel>(x))
                .ToList();

            var categoryCount = database.Authors.Count();

            return Task.FromResult(new BasePagingData<TagSummaryModel>()
            {
                Items = tags,
                PageSize = query.PageSize ?? 1,
                PageIndex = query.PageIndex ?? 20,
                TotalItem = categoryCount,
                TotalPage = (int)Math.Ceiling((double)categoryCount / (query.PageSize ?? 20))
            });
        }
    }
}
