using BlogCore.Business.Interfaces;
using BlogCore.Business.Models;
using BlogCore.Business.Notificacoes;
using BlogCore.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Business.Services
{
    public class AutorService(AppDbContext db, INotificador notificador, UserManager<IdentityUser> userManager) : IAutorService
    {
        private readonly AppDbContext _context = db;
        private readonly INotificador _notificador = notificador;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        public async Task Criar(Autor autor, string userId)
        {
            var autorExiste = await _context.Autores.FirstOrDefaultAsync(a => a.UsuarioId == userId);

            if (autorExiste != null)
            {
                _notificador.Adicionar(new Notificacao("Autor já cadastrado."));
                return;
            }

            var user = await _userManager.FindByIdAsync(userId);

            autor.Usuario = user;

            await _context.Autores.AddAsync(autor);
            await _context.SaveChangesAsync();
        }
    }
}
