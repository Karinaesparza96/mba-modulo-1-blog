using BlogCore.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Configurations;

public static class DbContextConfig
{
    public static WebApplicationBuilder AddDbContextConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        return builder;
    }
}