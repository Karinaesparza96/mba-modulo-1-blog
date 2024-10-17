using BlogApp.ViewsModels;
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
            Message = "Ocorreu um erro inesperado, tente novamente mais tarde ou contate nosso suporte.",

        };

        if (statusCode == 404)
        {
            errorViewModel.Message = "Não foi possível encontrar está página.";
        }

        if (statusCode == 403)
        {
            errorViewModel.Message = "Você não tem permissão para realizar esta operação.";
        }

        return View("Error", errorViewModel);
    }
}
