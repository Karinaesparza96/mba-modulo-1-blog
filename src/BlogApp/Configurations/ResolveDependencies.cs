using BlogApp.Extensions;
using BlogCore.Business.Interfaces;
using BlogCore.Business.Notificacoes;
using BlogCore.Data.Repositories;

namespace BlogApp.Configurations
{
    public static class ResolveDependencies
    {
        public static IServiceCollection ResolveDependencieInjection(this IServiceCollection services)
        {
            services.AddScoped<IAppIdentityUser, AppIdentityUser>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IComentarioRepository, ComentarioRepository>();
            services.AddScoped<INotificador, Notificador>();



            return services;
        }
    }
}
