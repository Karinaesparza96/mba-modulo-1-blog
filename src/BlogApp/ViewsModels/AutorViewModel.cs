﻿using Microsoft.AspNetCore.Identity;

namespace BlogApp.ViewsModels
{
    public class AutorViewModel
    {   
        public long Id { get; set; }
        public string? UsuarioId { get; set; }
        public string? Nome { get; set; }
    }
}
