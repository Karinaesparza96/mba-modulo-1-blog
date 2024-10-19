using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs;

public class RegisterUserDto
{
    [Required(ErrorMessage = "Campo {0} obrigatório.")]
    [EmailAddress(ErrorMessage = "Campo {0} está em formato inválido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Campo {0} obrigatório")]
    [StringLength(100, ErrorMessage = "Campo {0} precisa ter entre {1} e {2} caracteres.", MinimumLength = 6)]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "As senhas não conferem.")]
    public string? ConfirmPassword { get; set; }
}
