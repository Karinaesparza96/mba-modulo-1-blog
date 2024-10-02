using BlogApp.ViewsModels;
using BlogCore.Business.Interfaces;

namespace BlogApp.Configurations;

public static class ComentarioViewModelExtensions
{
    public static ComentarioViewModel DefinirPermissao(this ComentarioViewModel comentario, IAppIdentityUser userApp, string autorUsuarioId)
    {
        comentario.TemPermissao = userApp.HasPermission(comentario.UsuarioId, autorUsuarioId);
        return comentario;
    }

    public static IEnumerable<ComentarioViewModel> DefinirPermissoes(this IEnumerable<ComentarioViewModel> comentarios, IAppIdentityUser userApp, string autorUsuarioId)
    {
        foreach (var comentario in comentarios)
        {
            comentario.DefinirPermissao(userApp, autorUsuarioId);
        }
        return comentarios;
    }
}
