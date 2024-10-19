using BlogApp.ViewsModels;
using BlogCore.Business.Messages;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;
public class ErrorController : Controller
{
    [Route("Error/{statusCode?}")]
    public IActionResult Index(int? statusCode)
    {
        var errorViewModel = new ErrorViewModel
        {
            Title = "Ops!",
            Message = Messages.BadRequestGeneric,

        };

        if (statusCode == 404)
        {
            errorViewModel.Message = Messages.RegistroNaoEncontrado;
        }

        if (statusCode == 403)
        {
            errorViewModel.Message = Messages.AcessoNaoAutorizado;
        }

        return View("Error", errorViewModel);
    }
}
