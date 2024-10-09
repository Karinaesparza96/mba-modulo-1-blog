using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs;

public class ComentarioDto
{
    public long Id { get; set; }
    public string? NomeUsuario { get; set; }
    public string? UsuarioId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public long PostId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(150, ErrorMessage = "O campo {0} precisa estar entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string? Conteudo { get; set; }
    public DateTime DataPublicacao { get; set; }
}
