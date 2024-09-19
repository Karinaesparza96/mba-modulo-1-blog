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
            services.AddScoped<INotificador, Notificador>();

            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IAutorService, AutorService>();

            services.AddScoped<IAppIdentityUser, AppIdentityUser>();

            return services;
        }
    }
}
