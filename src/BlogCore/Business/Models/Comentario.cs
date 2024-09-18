using Microsoft.AspNetCore.Identity;

namespace BlogCore.Business.Models
{
    public class Comentario : Entity
    {
        public string? Conteudo { get; set; }

        public int PostId { get; set; }
        public Post? Post { get; set; }

        public string? UsuarioId { get; set; }
        public IdentityUser? Usuario { get; set; }

    }
}