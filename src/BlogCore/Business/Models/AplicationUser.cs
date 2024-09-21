using Microsoft.AspNetCore.Identity;

namespace BlogCore.Business.Models
{
    public class AplicationUser : IdentityUser
    {
        public string? NomeCompleto { get; set; }
        public string? Biografia { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}