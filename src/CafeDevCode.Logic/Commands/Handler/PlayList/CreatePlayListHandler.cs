using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class CreatePlayListHandler 
        : IRequestHandler<CreatePlayList, BaseCommandResultWithData<PlayList>>
    {
        private readonly IMapper mapper;
        private readonly AppDatabase database;

        public CreatePlayListHandler(IMapper mapper,
            AppDatabase database)
        {
            this.mapper = mapper;
            this.database = database;
        }

        public Task<BaseCommandResultWithData<PlayList>> Handle(CreatePlayList request,
            CancellationToken cancellationToken)
        {
            var result = new BaseCommandResultWithData<PlayList>();

            try
            {
                var playList = mapper.Map<PlayList>(request);
                database.PlayLists.Add(playList);

                playList.SetCreateInfo(request.UserName ?? string.Empty, AppGlobal.SysDateTime);

                database.SaveChanges();

                result.Data = playList;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Messages = ex.Message;
            }

            return Task.FromResult(result);
        }
    }
}
