using AutoMapper;
using CafeDevCode.Common.Shared.Model;
using CafeDevCode.Database;
using CafeDevCode.Logic.Queries.Interface;
using CafeDevCode.Logic.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Queries.Implement
{
    public class PostQueries : IPostQueries
    {
        private readonly AppDatabase database;
        private readonly IMapper mapper;

        public PostQueries(AppDatabase database,
            IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }
        public List<PostSummaryModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<PostSummaryModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public PostDetailModel? GetDetail(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PostDetailModel?> GetDetailAsync(int id)
        {
            throw new NotImplementedException();
        }

        public BasePagingData<PostSummaryModel> GetPaging(BaseQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<BasePagingData<PostSummaryModel>> GetPagingAsync(BaseQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
