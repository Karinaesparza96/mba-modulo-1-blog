using BlogCore.Business.Models;

namespace BlogCore.Business.Interfaces
{
    public interface IAutorService
    {
        Task Adicionar(Autor autor,string userId);
        Task<Autor?> ObterPorId(int id);
        Task Atualizar(Autor autor,string userId);
    }
}
