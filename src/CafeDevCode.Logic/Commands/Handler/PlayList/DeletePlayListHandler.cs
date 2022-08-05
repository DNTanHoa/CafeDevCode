using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class DeletePlayListHandler 
        : IRequestHandler<DeletePlayList, BaseCommandResult>
    {
        private readonly IMapper mapper;
        private readonly AppDatabase database;

        public DeletePlayListHandler(IMapper mapper,
            AppDatabase database)
        {
            this.mapper = mapper;
            this.database = database;
        }

        public Task<BaseCommandResult> Handle(DeletePlayList request,
            CancellationToken cancellationToken)
        {
            var result = new BaseCommandResult();

            try
            {
                var playList = database.PlayLists.FirstOrDefault(x => x.Id == request.Id);

                if (playList != null)
                {
                    playList.MarkAsDelete(request.UserName ?? string.Empty, AppGlobal.SysDateTime);

                    database.Update(playList);
                    database.SaveChanges();

                    result.Success = true;
                }
                else
                {
                    result.Messages = $"Can't not find play list with id is {request.Id}";
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
