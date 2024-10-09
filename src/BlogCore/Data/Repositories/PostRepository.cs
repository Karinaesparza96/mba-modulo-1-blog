using BlogCore.Business.Interfaces;
using BlogCore.Business.Models;
using BlogCore.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Data.Repositories
{
    public class PostRepository(AppDbContext db,
                            IAppIdentityUser userApp) : IPostRepository
    {
        public async Task<Post?> ObterPorId(long id)
        {
            var post = await db.Posts
                                .Include(p => p.Autor)
                                .ThenInclude(a => a.Usuario)
                                .Include(p => p.Comentarios)
                                .ThenInclude(c => c.Usuario)
                                .FirstOrDefaultAsync(p => p.Id == id);
            return post;
        }

        public async Task<IEnumerable<Post>> ObterTodos()
        {
            var posts = await db.Posts
                                .AsNoTracking()
                                .Include(p => p.Autor)
                                .ThenInclude(a => a.Usuario)
                                .Include(p => p.Comentarios)!
                                .ThenInclude(c => c.Usuario)
                                .OrderByDescending(p => p.DataCadastro)
                                .ToListAsync();
            return posts;
        }

        public async Task Adicionar(Post post)
        {
            var userId = userApp.GetUserId();
            var autor = await ObterAutorPorIdUsuario(userId);

            if (autor == null)
            {
                autor = new Autor { UsuarioId = userId };
                await db.Autores.AddAsync(autor);
            }
            post.Autor = autor;

            await db.Posts.AddAsync(post);

            await db.SaveChangesAsync();
        }

        public async Task Atualizar(Post post)
        {
            db.Posts.Update(post);
            await db.SaveChangesAsync();
        }

        public async Task Remover(Post post)
        {
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
        }

        private async Task<Autor?> ObterAutorPorIdUsuario(string userId)
        {
            return await db.Autores.FirstOrDefaultAsync(a => a.UsuarioId == userId);
        }
    }
}
