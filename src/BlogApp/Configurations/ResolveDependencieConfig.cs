using BlogCore.Business.Interfaces;
using BlogCore.Data.Repositories;
using BlogCore.Extensions;

namespace BlogApp.Configurations
{
    public static class ResolveDependencieConfig
    {
        public static IServiceCollection AddResolveDependencie(this IServiceCollection services)
        {
            services.AddScoped<IAppIdentityUser, AppIdentityUser>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IComentarioRepository, ComentarioRepository>();

            return services;
        }
    }
}
