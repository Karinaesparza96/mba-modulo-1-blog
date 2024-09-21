using BlogCore.Business.Interfaces;
using BlogCore.Business.Models;
using BlogCore.Data.Context;

namespace BlogCore.Business.Services
{
    public class ComentarioService(AppDbContext db) : IComentarioService
    {
        private readonly AppDbContext _context = db;

        public async Task Adicionar(Comentario comentario)
        {   
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();
        }
    }
}
