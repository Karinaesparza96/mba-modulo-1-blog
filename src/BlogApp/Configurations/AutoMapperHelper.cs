using BlogApp.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace BlogCore.Configuration
{
    public static class AutoMapperHelper
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperConfiguration).Assembly);
            return services;
        }
    }
}
