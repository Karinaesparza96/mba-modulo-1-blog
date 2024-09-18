namespace BlogCore.Business.Models
{
    public class Post : Entity
    {
        public string? Titulo { get; set; }
        public string? Conteudo { get; set; }
        public int AutorId { get; set; }
        public required Autor Autor { get; set; }
        public ICollection<Comentario>? Comentarios { get; set; }
    }
}
