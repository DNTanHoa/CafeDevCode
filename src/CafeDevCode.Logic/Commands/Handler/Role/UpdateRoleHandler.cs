using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class UpdateRoleHandler : IRequestHandler<CreateRole, BaseCommandResultWithData<IdentityRole>>
    {
        private readonly IMapper mapper;
        private readonly AppDatabase database;
        private readonly RoleManager<IdentityRole> roleManager;

        public UpdateRoleHandler(IMapper mapper,
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
                var role = database.Roles.FirstOrDefault(r => r.Id == request.Id);

                if(role != null)
                {

                }
                else
                {

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
