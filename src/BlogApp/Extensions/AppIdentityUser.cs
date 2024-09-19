using BlogCore.Business.Interfaces;
using System.Security.Claims;

namespace BlogApp.Extensions
{
    public class AppIdentityUser(IHttpContextAccessor accessor) : IAppIdentityUser
    {
        private readonly IHttpContextAccessor _accessor = accessor;

        public string GetUserId()
        {
            if (!IsAuthenticated()) return string.Empty;

            var claim = _accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return claim ?? string.Empty;
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext?.User.Identity is { IsAuthenticated: true };
        }

        public bool IsAdmin()
        {
            return _accessor.HttpContext?.User.IsInRole("Admin") ?? false;
        }
    }
}
