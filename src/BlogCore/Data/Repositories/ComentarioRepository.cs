using BlogCore.Business.Interfaces;
using BlogCore.Business.Models;
using BlogCore.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Data.Repositories
{
    public class ComentarioRepository(AppDbContext db, IAppIdentityUser userApp) : IComentarioRepository
    {
        public async Task<Comentario?> ObterPorId(long id, long postId)
        {
            return await db.Comentarios
                .Include(c => c.Usuario)
                .Include(c => c.Post)
                .ThenInclude(p => p!.Autor)
                .Where(c => c.PostId == postId)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Comentario>> ObterTodosPorPost(long postId)
        {
            return await db.Comentarios
                    .AsNoTracking()
                    .Include(c => c.Usuario)
                    .Where(c => c.PostId == postId)
                    .OrderBy(c => c.DataCadastro)
                    .ToListAsync();
        }
        public async Task Adicionar(Comentario comentario)
        {
            var userId = userApp.GetUserId();
            var usuario = await db.Users.FindAsync(userId);

            comentario.UsuarioId = userId;
            comentario.Usuario = usuario;

            db.Comentarios.Add(comentario);
            await db.SaveChangesAsync();
        }

        public async Task Atualizar(Comentario comentario)
        {
            db.Comentarios.Update(comentario);
            await db.SaveChangesAsync();
        }

        public async Task Remover(Comentario comentario)
        {
            db.Comentarios.Remove(comentario);
            await db.SaveChangesAsync();
        }
    }
}
