using BlogCore.Business.Models;

namespace BlogCore.Business.Interfaces
{
    public interface IAutorService
    {
        Task Criar(Autor autor,string userId);
    }
}
