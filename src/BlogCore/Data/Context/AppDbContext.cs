using BlogCore.Business.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogCore.Data.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Autor> Autores { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var properties = builder.Model.GetEntityTypes()
                                        .SelectMany(e => e.GetProperties())
                                        .Where(p => p.ClrType == typeof(string));

            foreach (var property in properties)
            {
                property.SetColumnType("varchar(100)");
            }

            var softDeleteEntities = typeof(Entity).Assembly.GetTypes()
                                    .Where(type => typeof(Entity)
                                                       .IsAssignableFrom(type)
                                                   && type.IsClass
                                                   && !type.IsAbstract);

            foreach (var softDeleteEntity in softDeleteEntities)
            {
                var parameter = Expression.Parameter(softDeleteEntity, "x");
                var excluidoProperty = Expression.Property(parameter, "Excluido");
                var excluidoCheck = Expression.Equal(excluidoProperty, Expression.Constant(false));

                var lambda = Expression.Lambda(excluidoCheck, parameter);
                builder.Entity(softDeleteEntity).HasQueryFilter(lambda);
            }

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(builder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellation = default)
        {   
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Entity)
                {
                    if (entry.State == EntityState.Added)
                    {   
                        entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                    }
                    if (entry.State == EntityState.Modified)
                    {
                        entry.Property("DataAtualizacao").CurrentValue = DateTime.Now;
                        entry.Property("DataCadastro").IsModified = false;
                    }

                    if (entry.State == EntityState.Deleted)
                    {
                        entry.Property("Excluido").CurrentValue = true;
                        entry.Property("DataAtualizacao").CurrentValue = DateTime.Now;
                        entry.Property("DataCadastro").IsModified = false;
                        entry.State = EntityState.Modified;
                    }
                }

            }
            return base.SaveChangesAsync(cancellation);
        }

    }
}
