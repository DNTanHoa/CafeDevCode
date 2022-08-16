using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class CreateUserHandler : IRequestHandler<CreateUser, BaseCommandResultWithData<User>>
    {
        private readonly IMapper mapper;
        private readonly AppDatabase database;
        private readonly UserManager<User> userManager;

        public CreateUserHandler(IMapper mapper,
            AppDatabase database,
            UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.database = database;
            this.userManager = userManager;
        }

        public Task<BaseCommandResultWithData<User>> Handle(CreateUser request,
            CancellationToken cancellationToken)
        {
            var result = new BaseCommandResultWithData<User>();

            try
            {
                var user = mapper.Map<User>(request);
                
                var createResult = userManager.CreateAsync(user, request.Password);
                
                if (createResult.Result.Succeeded)
                {
                    result.Data = user;
                    result.Success = true;
                }
                else
                {
                    result.Messages = string.Join("-", createResult.Result.Errors.Select(x => x.Description));
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
