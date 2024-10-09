using BlogCore.Business.Interfaces;

namespace BlogApp.ViewsModels;

public static class PostViewModelExtension
{
    public static PostViewModel DefinirPermissao(this PostViewModel post, IAppIdentityUser userApp)
    {
        post.TemPermissao = userApp.IsOwnerOrAdmin(post.Autor?.UsuarioId);
        return post;
    }

    public static IEnumerable<PostViewModel> DefinirPermissoes(this IEnumerable<PostViewModel> posts, IAppIdentityUser userApp)
    {
        foreach (var post in posts)
        {
            DefinirPermissao(post, userApp);
        }
        return posts;
    }
}
