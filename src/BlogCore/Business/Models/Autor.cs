using Microsoft.AspNetCore.Identity;

namespace BlogCore.Business.Models
{
    public class Autor : Entity
    {
        public string? NomeCompleto { get; set; }
        public string? Biografia { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? UsuarioId { get; set; }
        public IdentityUser? Usuario { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}