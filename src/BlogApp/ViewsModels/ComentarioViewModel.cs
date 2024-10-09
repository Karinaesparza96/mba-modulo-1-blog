using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewsModels
{
    public class ComentarioViewModel
    {   
        public long Id { get; set; }
        public string? NomeUsuario { get; set; }
        public long PostId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(150, ErrorMessage = "O campo {0} precisa estar entre {2} e {1} caracteres.", MinimumLength = 2)]
        [DisplayName("Conteúdo")]
        public string? Conteudo { get; set; }
        public DateTime DataPublicacao { get; set; }
    }
}
