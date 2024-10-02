using BlogCore.Business.Interfaces;
using System.Security.Claims;

namespace BlogApp.Extensions
{
    public class AppIdentityUser(IHttpContextAccessor accessor) : IAppIdentityUser
    {
        public string GetUserId()
        {
            if (!IsAuthenticated()) return string.Empty;

            var claim = accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return claim ?? string.Empty;
        }

        public bool IsAuthenticated()
        {
            return accessor.HttpContext?.User.Identity is { IsAuthenticated: true };
        }

        public bool IsAdmin()
        {
            return accessor.HttpContext?.User.IsInRole("Admin") ?? false;
        }

        public bool IsOwnerOrAdmin(string? userId)
        {
            if (string.IsNullOrEmpty(userId)) return false;
            
            return userId == GetUserId() || IsAdmin();
        }

        public bool HasPermission(string? userIdComentario, string? userIdAutor)
        {
            var userId = GetUserId();

            if (IsAdmin()) return true;

            return userIdComentario == userId || userIdAutor == userId;
        }
    }
}
