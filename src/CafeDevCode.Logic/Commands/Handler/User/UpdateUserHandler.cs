using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class UpdateUserHandler 
        : IRequestHandler<UpdateUser, BaseCommandResultWithData<User>>
    {
        private readonly IMapper mapper;
        private readonly AppDatabase database;
        private readonly UserManager<User> userManager;

        public UpdateUserHandler(IMapper mapper,
            AppDatabase database,
            UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.database = database;
            this.userManager = userManager;
        }

        public Task<BaseCommandResultWithData<User>> Handle(UpdateUser request,
            CancellationToken cancellationToken)
        {
            var result = new BaseCommandResultWithData<User>();

            try
            {
                var user = userManager.FindByNameAsync(request.UpdateUserName).Result;

                if(user != null)
                {
                    user.Email = request.Email;
                    user.PhoneNumber = request.PhoneNumber;
                    user.AuthorId = request.AuthorId;

                    var updateResult = userManager.UpdateAsync(user).Result;

                    if(updateResult.Succeeded)
                    {
                        result.Data = user;
                        result.Success = true;
                    }
                    else
                    {
                        result.Messages = string.Join("-", updateResult.Errors.Select(x => x.Description));
                    }
                }
                else
                {
                    result.Messages = "Can't not find user to update";
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
