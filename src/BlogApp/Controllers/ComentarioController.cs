using AutoMapper;
using BlogApp.ViewsModels;
using BlogCore.Business.Interfaces;
using BlogCore.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    [Route("posts/{postId:long}/comentarios")]
    public class ComentarioController(INotificador notificador,
                                      IComentarioService comentarioService,
                                      IPostService postService,
                                      IMapper mapper
        ) : MainController(notificador)
    {   
        private readonly IComentarioService _comentarioService = comentarioService;
        private readonly IPostService _postService = postService;
        private readonly IMapper _mapper = mapper;

        [Authorize, HttpPost("")]
        public async Task<IActionResult> Create(long postId, ComentarioViewModel comentarioViewModel)
        {
            if (!ModelState.IsValid)
            {
                //TempData["ModelStateErrors"] = ModelState;

                // Redireciona para a tela de detalhes do post, mantendo o postId
                return RedirectToAction("Details", "Posts", new { id = comentarioViewModel.PostId });
            }

            await _comentarioService.Adicionar(_mapper.Map<Comentario>(comentarioViewModel));

            return RedirectToAction("Details", "Posts", new { id = comentarioViewModel.PostId });
        }
    }
}
