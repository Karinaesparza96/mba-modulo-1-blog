using BlogCore.Business.Interfaces;
using BlogCore.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class MainController(INotificador notificador) : Controller
    {
        private readonly INotificador _notificador = notificador;
        protected void Notificar(string mensagem)
        {
            _notificador.Adicionar(new Notificacao(mensagem));
        }

        protected IActionResult CustomResponse(object? model = default)
        {
            if (!OperacaoValida())
            {
               ViewBag.Messages = _notificador.ObterNotificacoes().Select(msg => msg.Mensagem);
            }

            return View(model);
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }
    }
}
