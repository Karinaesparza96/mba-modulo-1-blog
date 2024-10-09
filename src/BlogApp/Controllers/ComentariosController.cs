using AutoMapper;
using BlogApp.ViewsModels;
using BlogCore.Business.Interfaces;
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

            var postViewModel = mapper.Map<PostViewModel>(comentario.Post).DefinirPermissao(userApp);

            ViewBag.TemPermissao = postViewModel.TemPermissao;

            var comentarioViewModel = mapper.Map<ComentarioViewModel>(comentario);

            return PartialView("_ComentarioFormPartial", comentarioViewModel);
        }
        [Authorize, HttpPost("novo")]
        public async Task<IActionResult> Create(ComentarioViewModel comentarioViewModel, long postId)
        {   
            if (!ModelState.IsValid)
            {
                return PartialView("_ComentarioFormPartial", comentarioViewModel);
            }
            var post = await postRepository.ObterPorId(postId);

            if (post == null) return NotFound();

            var postViewModel = mapper.Map<PostViewModel>(post).DefinirPermissao(userApp);

            await comentarioRepository.Adicionar(mapper.Map<Comentario>(comentarioViewModel));

            ViewBag.TemPermissao = postViewModel.TemPermissao;

            return PartialView("_ComentarioListaPartial", mapper.Map<IEnumerable<ComentarioViewModel>>(post.Comentarios));
        }
        [Authorize, HttpGet("editar/{id:long}")]
        public async Task<IActionResult> Edit(long id, long postId)
        {
            var comentario = await comentarioRepository.ObterPorId(id, postId);

            if (comentario == null)
            {
                return NotFound();
            }

            var usuarioAutorizado = userApp.IsOwnerOrAdmin(comentario.Post?.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                return Forbid();
            }
            var comentarioViewModel = mapper.Map<ComentarioViewModel>(comentario);

            return PartialView("_ComentarioFormPartial", comentarioViewModel);
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
                return BadRequest();
            }

            var comentario = await comentarioRepository.ObterPorId(id, postId);

            if (comentario == null)
            {
                return NotFound();
            }

            var usuarioAutorizado = userApp.IsOwnerOrAdmin(comentario.Post?.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                return Forbid();
            }

            comentario.Conteudo = comentarioViewModel.Conteudo;

            await comentarioRepository.Atualizar(comentario);

            var comentarios = mapper.Map<IEnumerable<ComentarioViewModel>>(await comentarioRepository.ObterTodosPorPost(comentario.PostId));
            var postVewModel = mapper.Map<PostViewModel>(comentario.Post).DefinirPermissao(userApp);

            ViewBag.TemPermissao = postVewModel.TemPermissao;
            return PartialView("_ComentarioListaPartial", comentarios);
        }

        [Authorize, HttpPost("excluir/{id:long}")]
        public async Task<IActionResult> Delet(long id, long postId)
        {
            var comentario = await comentarioRepository.ObterPorId(id, postId);

            if (comentario == null)
            {
                return NotFound();
            }

            var usuarioAutorizado = userApp.IsOwnerOrAdmin(comentario.Post?.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                return Forbid();
            }

            await comentarioRepository.Remover(comentario);

            var comentarios = mapper.Map<IEnumerable<ComentarioViewModel>>(await comentarioRepository.ObterTodosPorPost(comentario.PostId));
            var postVewModel = mapper.Map<PostViewModel>(comentario.Post).DefinirPermissao(userApp);

            ViewBag.TemPermissao = postVewModel.TemPermissao;

            return PartialView("_ComentarioListaPartial", comentarios);
        }
    }
}
