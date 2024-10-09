using BlogCore.Business.Interfaces;
using BlogCore.Data.Repositories;
using BlogCore.Extensions;

namespace BlogApi.Configurations;

public static class ResolveDependencieConfig
{
    public static WebApplicationBuilder AddResolveDependencie(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAppIdentityUser, AppIdentityUser>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<IComentarioRepository, ComentarioRepository>();

        return builder;
    }
}