using BlogCore.Business.Models;

namespace BlogCore.Business.Interfaces
{
    public interface IComentarioRepository
    {
        Task<Comentario?> ObterPorId(long id, long postId);

        Task<IEnumerable<Comentario>> ObterTodosPorPost(long postId);

        Task Adicionar(Comentario comentario);

        Task Atualizar(Comentario comentario);

        Task Remover(Comentario comentario);
    }
}
