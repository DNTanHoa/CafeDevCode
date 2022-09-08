using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.MappingProfile
{
    public class PlayListMappingProfile : Profile
    {
        public PlayListMappingProfile()
        {
            CreateMap<PlayList, PlayListSummaryModel>();
            CreateMap<PlayList, PlayListDetailModel>();
            CreateMap<CreatePlayList, PlayList>();
            CreateMap<UpdatePlayList, PlayList>();
        }
    }
}
