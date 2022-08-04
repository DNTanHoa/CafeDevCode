global using CafeDevCode.Logic.Shared.Models;
global using CafeDevCode.Logic.Commands.Request;
using AutoMapper;



namespace CafeDevCode.Logic.MappingProfile
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            CreateMap<CreateAuthor, Author>();
            CreateMap<UpdateAuthor, Author>();
            CreateMap<Author, AuthorSummaryModel>()
                .ReverseMap();
            CreateMap<Author, AuthorDetailModel>()
                .ReverseMap();
        }
    }
}
