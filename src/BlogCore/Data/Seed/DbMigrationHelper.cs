using BlogCore.Business.Models;
using BlogCore.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlogCore.Data.Seed;

public static class DbMigrationHelperExtension
{
    public static void UseDbMigrationHelper(this WebApplication app)
    {
        DbMigrationHelper.EnsureSeedData(app).Wait();
    }
}
public static class DbMigrationHelper
{
    public static async Task EnsureSeedData(WebApplication application)
    {
        var services = application.Services.CreateScope().ServiceProvider;
        await EnsureSeedData(services);
    }

    public static async Task EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (env.IsDevelopment())
        {
            await context.Database.MigrateAsync();

            await EnsureSeedTables(context);
        }
    }

    private static async Task EnsureSeedTables(AppDbContext context)
    {
        if (context.Posts.Any() || context.Users.Any()) return;

        var user = new IdentityUser
        {
            Id = "1",
            Email = "teste@teste.com",
            EmailConfirmed = true,
            NormalizedEmail = "TESTE@TESTE.COM",
            UserName = "teste@teste.com",
            AccessFailedCount = 0,
            PasswordHash = "AQAAAAIAAYagAAAAEF/nmfwFGPa8pnY9AvZL8HKI7r7l+aM4nryRB+Y3Ktgo6d5/0d25U2mhixnO4h/K5w==",
            NormalizedUserName = "TESTE@TESTE.COM"
        };

        var fulano = new IdentityUser
        {
            Id = "2",
            Email = "fulano@teste.com",
            EmailConfirmed = true,
            NormalizedEmail = "FULANO@TESTE.COM",
            UserName = "fulano@teste.com",
            AccessFailedCount = 0,
            PasswordHash = "AQAAAAIAAYagAAAAEF/nmfwFGPa8pnY9AvZL8HKI7r7l+aM4nryRB+Y3Ktgo6d5/0d25U2mhixnO4h/K5w==",
            NormalizedUserName = "FULANO@TESTE.COM"
        };
        var beltrano = new IdentityUser
        {
            Id = "3",
            Email = "beltrano@teste.com",
            EmailConfirmed = true,
            NormalizedEmail = "BELTRANO@TESTE.COM",
            UserName = "beltrano@teste.com",
            AccessFailedCount = 0,
            PasswordHash = "AQAAAAIAAYagAAAAEF/nmfwFGPa8pnY9AvZL8HKI7r7l+aM4nryRB+Y3Ktgo6d5/0d25U2mhixnO4h/K5w==",
            NormalizedUserName = "BELTRANO@TESTE.COM"
        };

        var post = new Post
        {
            Titulo = "What is .NET?",
            Conteudo =
                ".NET is a free, cross-platform, open source developer platform for building many different types of applications.\r\nWith .NET, you can use multiple languages, editors, and libraries to build for web, mobile, desktop, games, IoT, and more.",
            Autor = new Autor
            {
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now,
                Excluido = false,
                UsuarioId = user.Id
            },
            DataCadastro = DateTime.Now,
            DataAtualizacao = DateTime.Now,
            Comentarios = new List<Comentario>
            {
                new() {
                    DataCadastro = DateTime.Now,
                    DataAtualizacao = DateTime.Now,
                    Conteudo = "Muito legal!",
                    UsuarioId = fulano.Id
                },
                new() {
                    DataCadastro = DateTime.Now,
                    DataAtualizacao = DateTime.Now,
                    Conteudo = "Legal!",
                    UsuarioId = beltrano.Id
                }
            }
        };

        await context.Users.AddAsync(user);
        await context.Users.AddAsync(fulano);
        await context.Users.AddAsync(beltrano);
        await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
    }
}
