global using CafeDevCode.Database.Entities;
global using CafeDevCode.Logic.Commands.Request;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.MappingProfile
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CreateCategory, Category>();
            CreateMap<UpdateCategory, Category>();
            CreateMap<Category, CategorySummaryModel>()
                .ReverseMap();
            CreateMap<Category, CategoryDetailModel>()
                .ReverseMap();
        }
    }
}
