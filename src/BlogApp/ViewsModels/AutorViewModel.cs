using BlogCore.Business.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewsModels
{
    public class AutorViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa estar entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string? NomeCompleto { get; set; }

        [StringLength(200, ErrorMessage = "O campo {0} pode conter até {1} caracteres.")]
        public string? Biografia { get; set; }
        public DateTime? DataNascimento { get; set; }
        public int NumeroPublicacoes { get; set; }
        public string? UsuarioId { get; set; }
        public IdentityUser? Usuario { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
