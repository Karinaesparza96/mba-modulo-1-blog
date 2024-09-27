using BlogCore.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogCore.Data.Mappings
{
    public class PostMapping : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Titulo)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(p => p.Conteudo)
                .IsRequired()
                .HasColumnType("varchar(1000)");

            builder.HasMany(p => p.Comentarios)
                   .WithOne(c => c.Post);

            builder.HasOne(p => p.Autor);

            builder.ToTable("Posts");
        }
    }
}
