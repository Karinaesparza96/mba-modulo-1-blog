using Microsoft.AspNetCore.Identity;

namespace BlogCore.Business.Models
{
    public class Autor : Entity
    {
        public required string UsuarioId { get; set; }
        public IdentityUser Usuario { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}