namespace BlogCore.Business.Models
{
    public class Post : Entity
    {
        public string? Titulo { get; set; }
        public string? Conteudo { get; set; }
        public long AutorId { get; set; }
        public Autor Autor { get; set; } = null!;
        public ICollection<Comentario>? Comentarios { get; set; }
    }
}
