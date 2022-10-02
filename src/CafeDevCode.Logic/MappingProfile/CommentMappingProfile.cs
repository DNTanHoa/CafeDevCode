using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.MappingProfile
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<Comment, CommentModel>();
            CreateMap<CreateComment, Comment>()
                .ForMember(x => x.CreatedAt, y => y.Ignore());
            CreateMap<UpdateComment, Comment>()
                .ForMember(x => x.CreatedAt, y => y.Ignore());
        }
    }
}
