using System.Net;
using BlogCore.Business.Interfaces;
using BlogCore.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BlogApi.Controllers
{
    public abstract class BaseController(INotificador notificador) : ControllerBase
    {
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                NotificarErro(modelState);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object? result = null)
        {
            if (OperacaoValida())
            {
                return new ObjectResult(result)
                {
                    StatusCode = (int)statusCode
                };
            }

            return BadRequest(new
            {
                errors = notificador.ObterNotificacoes().Select(e => e.Mensagem)
            });
        }

        protected bool OperacaoValida()
        {
            return !notificador.TemNotificacao();
        }

        protected void NotificarErro(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(value => value.Errors);

            foreach (var error in errors)
            {
                var message = error.Exception != null ? error.Exception.Message : error.ErrorMessage;
                NotificarErro(message);
            }
        }

        protected void NotificarErro(string mensagemErro)
        {
            notificador.AdicionarNotificacao(new Notificacao(mensagemErro));
        }
    }
}
