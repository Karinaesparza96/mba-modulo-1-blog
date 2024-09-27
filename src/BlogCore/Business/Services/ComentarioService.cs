using BlogCore.Business.Interfaces;
using BlogCore.Business.Models;
using BlogCore.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Business.Services
{
    public class ComentarioService(AppDbContext db, IAppIdentityUser userApp) : IComentarioService
    {
        private readonly AppDbContext _context = db;
        private readonly IAppIdentityUser _userApp = userApp;

        public async Task<IEnumerable<Comentario>> Adicionar(Comentario comentario)
        {   
            var userId = _userApp.GetUserId();
            var user = await _context.Users.FindAsync(userId);

            comentario.Usuario = user;

            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            return await _context.Comentarios
                    .Where(c => c.PostId == comentario.PostId)
                    .OrderBy(c => c.DataCadastro)
                    .ToListAsync();
        }

        public Task Atualizar(Comentario comentario)
        {
            throw new NotImplementedException();
        }
    }
}
