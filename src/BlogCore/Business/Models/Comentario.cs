using Microsoft.AspNetCore.Identity;

namespace BlogCore.Business.Models
{
    public class Comentario : Entity
    {
        public string? Conteudo { get; set; }
        public long PostId { get; set; }
        public Post? Post { get; set; }
        public required string UsuarioId { get; set; }
        public IdentityUser Usuario { get; set; }

    }
}