using AutoMapper;
using BlogApp.Configurations;
using BlogApp.ViewsModels;
using BlogCore.Business.Interfaces;
using BlogCore.Business.MessagesDefault;
using BlogCore.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    [Route("/posts/{postId:long}/comentarios")]
    public class ComentariosController(IComentarioRepository comentarioRepository,
                                      IPostRepository postRepository,
                                      IAppIdentityUser userApp,
                                      IMapper mapper) : Controller
    {
        [HttpGet("detalhes/{id:long}")]
        public async Task<IActionResult> Details(long id, long postId)
        {
            var comentario = await comentarioRepository.ObterPorId(id, postId);

            if (comentario == null)
            {
                return NotFound();
            }

            var comentarioViewModel = mapper.Map<ComentarioViewModel>(comentario);
            
            return PartialView("_ComentarioFormPartial", comentarioViewModel);
        }
        [Authorize, HttpPost("novo")] // ValidateAntiForgeryToken
        public async Task<IActionResult> Create(ComentarioViewModel comentarioViewModel, long postId)
        {
            if (!ModelState.IsValid)
            { 
                return PartialView("_ComentarioFormPartial", comentarioViewModel);
            }
            var post = await postRepository.ObterPorId(postId);

            if (post == null) return NotFound();

            await comentarioRepository.Adicionar(mapper.Map<Comentario>(comentarioViewModel));

            var comentarios = await comentarioRepository.ObterTodosPorPost(postId);

            var comentariosViewModel = mapper.Map<IEnumerable<ComentarioViewModel>>(comentarios)
                                                .DefinirPermissoes(userApp, post.Autor.UsuarioId);

            return PartialView("_ComentarioListaPartial", comentariosViewModel);
        }
        [Authorize, HttpPost("editar/{id:long}")]
        public async Task<IActionResult> Edit(long id, ComentarioViewModel comentarioViewModel, long postId)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ComentarioFormPartial", comentarioViewModel);
            }
            if (id != comentarioViewModel.Id || postId != comentarioViewModel.PostId)
            {
                ModelState.AddModelError("", Messages.IdsDiferentes); 
                return  PartialView("_ComentarioFormPartial", comentarioViewModel);
            }

            var comentario = await comentarioRepository.ObterPorId(id, postId);

            if (comentario == null)
            {
                return NotFound();
            }

            var usuarioAutorizado = userApp.HasPermission(comentario.UsuarioId, comentario.Post.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                return Forbid();
            }

            comentario.Conteudo = comentarioViewModel.Conteudo;

            await comentarioRepository.Atualizar(comentario);

            var comentarios = mapper.Map<IEnumerable<ComentarioViewModel>>(await comentarioRepository.ObterTodosPorPost(comentario.PostId));

            return PartialView("_ComentarioListaPartial", comentarios.DefinirPermissoes(userApp, comentario.Post?.Autor.UsuarioId!));
        }

        [Authorize, HttpPost("excluir/{id:long}")]
        public async Task<IActionResult> Delet(long id, long postId)
        {
            var comentario = await comentarioRepository.ObterPorId(id, postId);

            if (comentario == null)
            {
                return NotFound();
            }

            var usuarioAutorizado = userApp.HasPermission(comentario.UsuarioId, comentario.Post.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                return Forbid();
            }

            await comentarioRepository.Remover(comentario);

            var comentarios = mapper.Map<IEnumerable<ComentarioViewModel>>(await comentarioRepository.ObterTodosPorPost(comentario.PostId));

            return PartialView("_ComentarioListaPartial", comentarios.DefinirPermissoes(userApp, comentario.Post?.Autor.UsuarioId!));
        }
    }
}
