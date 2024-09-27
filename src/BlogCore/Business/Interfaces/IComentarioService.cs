using BlogCore.Business.Models;

namespace BlogCore.Business.Interfaces
{
    public interface IComentarioService
    {
        Task<IEnumerable<Comentario>> Adicionar(Comentario comentario);

        Task Atualizar(Comentario comentario);
    }
}
