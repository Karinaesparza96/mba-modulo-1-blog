namespace BlogCore.Business.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
