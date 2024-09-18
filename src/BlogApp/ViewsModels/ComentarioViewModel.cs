﻿using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewsModels
{
    public class ComentarioViewModel
    {
        public string? NomeUsuario { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(150, ErrorMessage = "O campo {0} precisa estar entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string? Conteudo { get; set; }

        public DateTime DataPublicacao { get; set; }

    }
}
