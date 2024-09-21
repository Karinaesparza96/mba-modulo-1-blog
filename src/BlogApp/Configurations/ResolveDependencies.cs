using BlogApp.Extensions;
using BlogCore.Business.Interfaces;
using BlogCore.Business.Notificacoes;
using BlogCore.Business.Services;

namespace BlogApp.Configurations
{
    public static class ResolveDependencies
    {
        public static IServiceCollection ResolveDependencieInjection(this IServiceCollection services)
        {
            services.AddScoped<IAppIdentityUser, AppIdentityUser>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IComentarioService, ComentarioService>();
            services.AddScoped<INotificador, Notificador>();



            return services;
        }
    }
}
