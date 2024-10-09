namespace BlogCore.Business.Interfaces
{
    public interface IAppIdentityUser
    {
        public string GetUserId();
        bool IsAuthenticated();
        public bool IsAdmin();
        public bool IsOwnerOrAdmin(string? userIdAutor);
    }
}
