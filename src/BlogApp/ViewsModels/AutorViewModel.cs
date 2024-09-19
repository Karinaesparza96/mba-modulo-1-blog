using BlogCore.Business.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewsModels
{
    public class AutorViewModel
    {   
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa estar entre {2} e {1} caracteres.", MinimumLength = 2)]
        [Display(Name = "Nome Completo")]
        public string? NomeCompleto { get; set; }

        [StringLength(200, ErrorMessage = "O campo {0} pode conter até {1} caracteres.")]
        public string? Biografia { get; set; }

        [DataType(DataType.Date, ErrorMessage = "o campo {0} está em formato inválido.")]
        [Display(Name = "Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "Número de Publicações")]
        public int NumeroPublicacoes { get; set; }

        public string? UsuarioId { get; set; }

        public IdentityUser? Usuario { get; set; }

        public ICollection<Post>? Posts { get; set; }
    }
}
