using BlogCore.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Configurations
{
    public static class DbContextConfiguration
    {
        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
            return services;
        }
    }
}
