using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class CreateRoleHandler
        : IRequestHandler<CreateRole, BaseCommandResultWithData<IdentityRole>>
    {
        private readonly IMapper mapper;
        private readonly AppDatabase database;
        private readonly RoleManager<IdentityRole> roleManager;

        public CreateRoleHandler(IMapper mapper,
            AppDatabase database,
            RoleManager<IdentityRole> roleManager)
        {
            this.mapper = mapper;
            this.database = database;
            this.roleManager = roleManager;
        }

        public Task<BaseCommandResultWithData<IdentityRole>> Handle(
            CreateRole request, CancellationToken cancellationToken)
        {
            var result = new BaseCommandResultWithData<IdentityRole>();

            try
            {
                if(!roleManager.RoleExistsAsync(request.Name).Result)
                {
                    roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = request.Name,
                    });

                    result.Success = true;
                }
                else
                {
                    result.Messages = $"Role with {request.Name} has exist";
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
