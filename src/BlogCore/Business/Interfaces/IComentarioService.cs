using BlogCore.Business.Models;

namespace BlogCore.Business.Interfaces
{
    public interface IComentarioService
    {
        Task Adicionar(Comentario comentario);
    }
}
