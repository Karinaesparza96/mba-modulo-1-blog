using System.Security.Claims;
using BlogCore.Business.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BlogCore.Extensions;

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

    public bool IsOwnerOrAdmin(string? userIdAutor)
    {
        if (string.IsNullOrEmpty(userIdAutor)) return false;
        
        return userIdAutor == GetUserId() || IsAdmin();
    }
}
