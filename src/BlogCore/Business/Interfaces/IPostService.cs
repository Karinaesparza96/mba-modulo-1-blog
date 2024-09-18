using BlogCore.Business.Models;

namespace BlogCore.Business.Interfaces
{
    public interface IPostService
    {
        Task<Post?> ObterPorId(int id);
        Task<IEnumerable<Post>> ObterTodos();
        Task Adicionar(Post post, string? userId);
        Task Atualizar(int id, Post post);
        Task Remover(int id);

    }
}
