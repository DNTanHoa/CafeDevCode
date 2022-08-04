﻿using AutoMapper;
using CafeDevCode.Database;
using CafeDevCode.Ultils.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthor,
        BaseCommandResultWithData<Author>>
    {
        private readonly AppDatabase database;
        private readonly IMapper mapper;

        public UpdateAuthorHandler(AppDatabase database,
            IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }

        public Task<BaseCommandResultWithData<Author>> Handle(
            UpdateAuthor request, CancellationToken cancellationToken)
        {
            var result = new BaseCommandResultWithData<Author>();

            try
            {
                var author = database.Authors.FirstOrDefault(x => x.Id == request.Id);
                
                if(author != null)
                {
                    mapper.Map(request, author);
                    author.SetUpdateInfo(request.UserName ?? string.Empty, AppGlobal.SysDateTime);
                    database.Update(author);
                    database.SaveChanges();

                    result.Success = true;
                }
                else
                {
                    result.Messages = $"Can't not find author with id is {request.Id}";
                }
            }
            catch(Exception ex)
            {
                result.Messages = ex.Message;
            }

            return Task.FromResult(result);
        }
    }
}
