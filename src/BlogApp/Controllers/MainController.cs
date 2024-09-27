using BlogCore.Business.Interfaces;
using BlogCore.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class MainController(INotificador notificador) : Controller
    {
        protected void Notificar(string mensagem)
        {
            notificador.Adicionar(new Notificacao(mensagem));
        }

        protected IActionResult CustomResponse(object? model = default)
        {
            if (!OperacaoValida()) ViewBag.Messages = ObterNotificacaoes();

            return View(model);
        }

        protected bool OperacaoValida()
        {
            return !notificador.TemNotificacao();
        }

        protected IEnumerable<string> ObterNotificacaoes()
        {
            return notificador.ObterNotificacoes().Select(msg => msg.Mensagem);
        }
    }
}
