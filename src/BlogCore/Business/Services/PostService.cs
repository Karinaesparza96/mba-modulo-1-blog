using BlogCore.Business.Interfaces;
using BlogCore.Business.MessagesDefault;
using BlogCore.Business.Models;
using BlogCore.Business.Notificacoes;
using BlogCore.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Business.Services
{
    public class PostService(AppDbContext db, 
                            INotificador notificador, 
                            IAppIdentityUser userApp) : IPostService
    {
        private readonly AppDbContext _context = db;
        private readonly INotificador _notificador = notificador;
        private readonly IAppIdentityUser _userApp = userApp;
    
        public async Task<Post?> ObterPorId(long id)
        {
            var post = await _context.Posts
                            .Include(p => p.Autor)
                            .ThenInclude(a => a.Usuario)
                            .Include(p => p.Comentarios)
                            .FirstOrDefaultAsync(p => p.Id == id);
            return post ?? null;
        }
        public async Task<IEnumerable<Post>> ObterTodos()
        {   
            var posts = await _context.Posts
                            .Include(p => p.Autor)
                            .ThenInclude(a => a.Usuario)
                            .Include(p => p.Comentarios)
                            .OrderBy(p => p.DataCadastro)
                            .ToListAsync();
            return posts;
        }

        public async Task Adicionar(Post post)
        {
            var userId = _userApp.GetUserId();
            var autor = await ObterAutorPorIdUsuario(userId);

            if (autor == null)
            {
                autor = new Autor { UsuarioId = userId };
                await _context.Autores.AddAsync(autor);
            }
            post.Autor = autor;

            await _context.Posts.AddAsync(post);

            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Post post)
        {
            var postExiste = await ObterPostComAutor(post.Id);

            if (postExiste == null) 
            { 
                _notificador.Adicionar(new Notificacao(Messages.RegistroNaoEncontrado));
                return;
            };

            var usuarioAutorizado = _userApp.IsAuthorized(postExiste.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                _notificador.Adicionar(new Notificacao(Messages.AcaoRestritaAutorOuAdmin));
                return;
            }

            postExiste.Titulo = post.Titulo;
            postExiste.Conteudo = post.Conteudo;

             _context.Posts.Update(postExiste);
            await _context.SaveChangesAsync();
        }

        public async Task Remover(long id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                _notificador.Adicionar(new Notificacao(Messages.RegistroNaoEncontrado));
                return;
            }
            var usuarioAutorizado = _userApp.IsAuthorized(post.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                _notificador.Adicionar(new Notificacao(Messages.AcaoRestritaAutorOuAdmin));
                return;
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        private async Task<Autor?> ObterAutorPorIdUsuario(string userId)
        {
            return await _context.Autores.FirstOrDefaultAsync(a => a.UsuarioId == userId);
        }

        private async Task<Post?> ObterPostComAutor(long id)
        {
            return await _context.Posts
                .Include(p => p.Autor)
                .ThenInclude(a => a.Usuario)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
