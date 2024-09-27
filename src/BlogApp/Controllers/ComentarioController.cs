using AutoMapper;
using BlogApp.ViewsModels;
using BlogCore.Business.Interfaces;
using BlogCore.Business.MessagesDefault;
using BlogCore.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    [Route("comentarios")]
    public class ComentarioController(INotificador notificador,
                                      IComentarioService comentarioService,
                                      IPostService postService,
                                      IMapper mapper) : MainController(notificador)
    {
        [Authorize, HttpPost("novo"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComentarioViewModel comentarioViewModel)
        {
            if (!ModelState.IsValid) 
                return PartialView("_FormNovoComentario", comentarioViewModel);

            var comentarios = await comentarioService.Adicionar(mapper.Map<Comentario>(comentarioViewModel));

            return PartialView("_ComentarioPartial", mapper.Map<IEnumerable<ComentarioViewModel>>(comentarios));
        }
        [Authorize, HttpPost("editar/{id:long}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, ComentarioViewModel comentarioViewModel)
        {
            if (id != comentarioViewModel.Id)
            {
                Notificar(Messages.IdsDiferentes);
                ViewBag.Messages = ObterNotificacaoes();
                return  PartialView("_FormModalComentarioPartial", comentarioViewModel);
            }

            
            return View();
        }
    }
}
