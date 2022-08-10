global using CafeDevCode.Ultils.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class CreateCategoryHandler 
        : IRequestHandler<CreateCategory, BaseCommandResultWithData<Category>>
    {
        private readonly IMapper mapper;
        private readonly AppDatabase database;

        public CreateCategoryHandler(IMapper mapper,
            AppDatabase database)
        {
            this.mapper = mapper;
            this.database = database;
        }

        public Task<BaseCommandResultWithData<Category>> Handle(CreateCategory request,
            CancellationToken cancellationToken)
        {
            var result = new BaseCommandResultWithData<Category>();

            try
            {
                var category = mapper.Map<Category>(request);
                database.Categories.Add(category);

                category.SetCreateInfo(request.UserName ?? string.Empty, AppGlobal.SysDateTime);

                database.SaveChanges();

                result.Success = true;
                result.Data = category;
            }
            catch (Exception ex)
            {
                result.Messages = ex.Message;
            }

            return Task.FromResult(result);
        }
    }
}
