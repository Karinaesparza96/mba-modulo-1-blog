using BlogCore.Data.Context;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Configurations
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>()
                     .AddEntityFrameworkStores<AppDbContext>();
            return services;
        }
    }
}
