using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class DeleteUserHandler : IRequestHandler<DeleteUser, BaseCommandResult>
    {
        private readonly IMapper mapper;
        private readonly AppDatabase database;
        private readonly UserManager<User> userManager;

        public DeleteUserHandler(IMapper mapper,
            AppDatabase database,
            UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.database = database;
            this.userManager = userManager;
        }


        public Task<BaseCommandResult> Handle(DeleteUser request,
            CancellationToken cancellationToken)
        {
            var result = new BaseCommandResult();

            try
            {
                var user = userManager.FindByNameAsync(request.DeleteUserName).Result;

                if (user != null)
                {
                    var updateResult = userManager.DeleteAsync(user).Result;

                    if (updateResult.Succeeded)
                    {
                        result.Success = true;
                    }
                    else
                    {
                        result.Messages = string.Join("-", updateResult.Errors.Select(x => x.Description));
                    }
                }
                else
                {
                    result.Messages = "Can't not find user to delete";
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
