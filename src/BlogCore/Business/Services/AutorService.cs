using BlogCore.Business.Interfaces;
using BlogCore.Business.Models;
using BlogCore.Business.Notificacoes;
using BlogCore.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Business.Services
{
    public class AutorService(AppDbContext db, INotificador notificador, UserManager<IdentityUser> userManager) 
        : IAutorService
    {
        private readonly AppDbContext _context = db;
        private readonly INotificador _notificador = notificador;
        private readonly UserManager<IdentityUser> _userManager = userManager;

        public async Task<Autor?> ObterPorId(int id)
        {
            var autor = await _context.Autores.FindAsync(id);

            return autor;
        }
        public async Task Adicionar(Autor autor, string userId)
        {
            var autorExiste = await ObterAutorPorUsuarioIdVinculado(userId);

            if (autorExiste != null)
            {
                _notificador.Adicionar(new Notificacao("Autor já cadastrado."));
                return;
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                _notificador.Adicionar(new Notificacao("Usuário não encontrado."));
                return;
            }

            autor.Usuario = user;

            await _context.Autores.AddAsync(autor);
            await _context.SaveChangesAsync();
        }
        public async Task Atualizar(Autor autor, string userId)
        {
            var autorExiste = await ObterAutorPorUsuarioIdVinculado(userId);

            if (autorExiste == null)
            {
                _notificador.Adicionar(new Notificacao($"Não é possível {nameof(Atualizar)} o Autor de outro usuário."));
                return;
            }

            autorExiste.Biografia = autor.Biografia;
            autorExiste.DataNascimento = autor.DataNascimento;
            autorExiste.NomeCompleto = autor.NomeCompleto;

            _context.Autores.Update(autorExiste);
            await _context.SaveChangesAsync();
        }

        public async Task Remover(Autor autor, string userId)
        {
            var autorExiste = await ObterAutorPorUsuarioIdVinculado(userId);

            if (autorExiste == null)
            {
                _notificador.Adicionar(new Notificacao($"Não é possível {nameof(Remover)} o Autor de outro usuário."));
                return;
            }

            _context.Remove(autorExiste);
            await _context.SaveChangesAsync();
        }

        private async Task<Autor?> ObterAutorPorUsuarioIdVinculado(string userId)
        {
            return await _context.Autores.FirstOrDefaultAsync(a => a.UsuarioId == userId);
        }
    }
}
