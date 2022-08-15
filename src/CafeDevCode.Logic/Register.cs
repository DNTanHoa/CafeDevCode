using CafeDevCode.Logic.Queries.Implement;
using CafeDevCode.Logic.Queries.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic
{
    public static class Register
    {
        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddScoped<IAuthorQueries, AuthorQueries>();
            services.AddScoped<ICategoryQueries, CategoryQueries>();
            services.AddScoped<IPostQueries, PostQueries>();
            services.AddScoped<ITagQueries, TagQueries>();
            services.AddScoped<IPlayListQueries, PlayListQueries>();
            services.AddScoped<IVideoQueries, VideoQueries>();
            return services;
        }
    }
}
