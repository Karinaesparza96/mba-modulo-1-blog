using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewsModels
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa estar entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa estar entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string? Conteudo { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public AutorViewModel? Autor { get; set; }
        public IEnumerable<ComentarioViewModel>? Comentarios { get; set; }
    }
}
