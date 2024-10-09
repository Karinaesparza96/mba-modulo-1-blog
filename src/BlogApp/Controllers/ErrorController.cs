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
            Title = "Erro",
            Message = "Ocorreu um erro inesperado, tente novamente mais tarde ou contate nosso suporte",

        };

        if (statusCode == 404)
        {
            errorViewModel.Title = "Ops!";
            errorViewModel.Message = "Não foi possível encontrar está pagina.";
        }

        return View("Error", errorViewModel);
    }
}
