using BlogCore.Data.Context;
using Microsoft.AspNetCore.Identity;

namespace BlogApi.Configurations;

public static class IdentityConfig
{
    public static WebApplicationBuilder AddIdentityConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();
        return builder;
    }
}