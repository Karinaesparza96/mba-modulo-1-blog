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
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Conteudo)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasMany(p => p.Comentarios)
                   .WithOne(c => c.Post);

            builder.HasOne(p => p.Autor);

            builder.ToTable("Posts");
        }
    }
}
