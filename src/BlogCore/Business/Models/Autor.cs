using Microsoft.AspNetCore.Identity;

namespace BlogCore.Business.Models
{
    public class Autor : Entity
    {
        public string UsuarioId { get; set; }
        public IdentityUser Usuario { get; set; }
    }
}