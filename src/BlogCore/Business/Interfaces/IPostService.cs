using BlogCore.Business.Models;

namespace BlogCore.Business.Interfaces
{
    public interface IPostService
    {
        Task<Post?> ObterPorId(long id);
        Task<IEnumerable<Post>> ObterTodos();
        Task Adicionar(Post post, string? userId);
        Task Atualizar(Post post);
        Task Remover(long id);
    }
}
