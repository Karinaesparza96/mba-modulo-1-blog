namespace BlogCore.Business.Interfaces
{
    public interface IAppIdentityUser
    {
        public string GetUserId();
        bool IsAuthenticated();
        public bool IsAdmin();
        public bool HasPermission(string? userIdComentario, string userIdAutor);
        public bool IsOwnerOrAdmin(string? userId);
    }
}
