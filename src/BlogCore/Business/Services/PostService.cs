using BlogCore.Business.Interfaces;
using BlogCore.Business.Models;
using BlogCore.Business.Notificacoes;
using BlogCore.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Business.Services
{
    public class PostService(AppDbContext db, INotificador notificador) : IPostService
    {
        private readonly AppDbContext _context = db;
        private readonly INotificador _notificador = notificador;

        public async Task<Post?> ObterPorId(int id)
        {
            var post = await _context.Posts
                            .Include(p => p.Autor)
                            .Include(p => p.Comentarios)
                            .FirstOrDefaultAsync(p => p.Id == id);

            return post ?? null;
        }
        public async Task<IEnumerable<Post>> ObterTodos()
        {   
            var posts = await _context.Posts
                            .Include(p => p.Autor)
                            .Include(p => p.Comentarios)
                            .OrderBy(p => p.DataCadastro)
                            .ToListAsync();

            return posts;
        }

        public async Task Adicionar(Post post, string? userId)
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(a => a.UsuarioId == userId);

            if (autor == null)
            {
                _notificador.Adicionar(new Notificacao("Seu usuário não possui um Autor associado."));
                return;
            };

            post.Autor = autor;

            await _context.Posts.AddAsync(post);

            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(int id, Post post)
        {
            var postExiste = await _context.Posts.FindAsync(id);

            if (postExiste == null) return;

            postExiste.Titulo = post.Titulo;
            postExiste.Conteudo = post.Conteudo;

             _context.Posts.Update(postExiste);
            await _context.SaveChangesAsync();
        }

        public async Task Remover(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null) return;

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}
