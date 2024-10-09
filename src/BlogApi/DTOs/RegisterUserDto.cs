using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs;

public class RegisterUserDto
{
    [Required(ErrorMessage = "Campo {0} obrigatorio")]
    [EmailAddress(ErrorMessage = "Campo {0} esta em formato invalido")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Campo {0 obrigatorio}")]
    [StringLength(100, ErrorMessage = "Campo {0} precisa ter entre {1} e {2} caracteres", MinimumLength = 6)]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "As senhas nao conferem")]
    public string? ConfirmPassword { get; set; }
}
