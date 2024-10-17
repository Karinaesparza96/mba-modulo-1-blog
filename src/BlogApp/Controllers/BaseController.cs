using BlogCore.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

public abstract class BaseController(IAppIdentityUser userApp) : Controller
{
    protected RedirectToActionResult RedirectPageErrorForbidden()
    {
        return RedirectToAction("Index", "Error", new { statusCode = 403 });
    }

    protected bool IsOwnerOrAdmin(string? userIdAutor)
    {
        return userApp.IsOwnerOrAdmin(userIdAutor);
    }
}
