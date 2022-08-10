using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class CreateTagHandler 
        : IRequestHandler<CreateTag, BaseCommandResultWithData<Tag>>
    {
        private readonly IMapper mapper;
        private readonly AppDatabase database;

        public CreateTagHandler(IMapper mapper,
            AppDatabase database)
        {
            this.mapper = mapper;
            this.database = database;
        }

        public Task<BaseCommandResultWithData<Tag>> Handle(CreateTag request,
            CancellationToken cancellationToken)
        {
            var result = new BaseCommandResultWithData<Tag>();

            try
            {
                var tag = mapper.Map<Tag>(request);
                database.Tags.Add(tag);

                tag.SetCreateInfo(request.UserName ?? string.Empty, AppGlobal.SysDateTime);

                database.SaveChanges();
                result.Success = true;
                result.Data = tag;
            }
            catch (Exception ex)
            {
                result.Messages = ex.Message;
            }

            return Task.FromResult(result);
        }
    }
}
