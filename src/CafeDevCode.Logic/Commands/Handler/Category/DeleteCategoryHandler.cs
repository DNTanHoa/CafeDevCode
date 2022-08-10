using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class DeleteCategoryHandler 
        : IRequestHandler<DeleteCategory, BaseCommandResult>
    {
        private readonly IMapper mapper;
        private readonly AppDatabase database;

        public DeleteCategoryHandler(IMapper mapper,
            AppDatabase database)
        {
            this.mapper = mapper;
            this.database = database;
        }

        public Task<BaseCommandResult> Handle(DeleteCategory request,
            CancellationToken cancellationToken)
        {
            var result = new BaseCommandResult();

            try
            {
                var category = database.Categories.FirstOrDefault(x => x.Id == request.Id);

                if (category != null)
                {
                    category.MarkAsDelete(request.UserName ?? string.Empty, AppGlobal.SysDateTime);

                    database.Update(category);
                    database.SaveChanges();

                    result.Success = true;
                }
                else
                {
                    result.Messages = $"Can't not find category with id is {request.Id}";
                }
            }
            catch (Exception ex)
            {
                result.Messages = ex.Message;
            }

            return Task.FromResult(result);
        }
    }
}
