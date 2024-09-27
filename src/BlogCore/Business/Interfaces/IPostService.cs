using BlogCore.Business.Models;

namespace BlogCore.Business.Interfaces
{
    public interface IPostService
    {
        Task<Post?> ObterPorId(long id);
        Task<IEnumerable<Post>> ObterTodos();
        Task Adicionar(Post post);
        Task Atualizar(Post post);
        Task Remover(long id);
    }
}
