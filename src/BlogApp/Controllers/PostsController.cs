using AutoMapper;
using BlogApp.ViewsModels;
using BlogCore.Business.Interfaces;
using BlogCore.Business.MessagesDefault;
using BlogCore.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    [Route("posts")]
    public class PostsController(IPostService postsService, 
                                IMapper mapper, 
                                INotificador notificador,
                                IAppIdentityUser userApp) : MainController(notificador)
    {
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var posts = await postsService.ObterTodos();

            return View(mapper.Map<IEnumerable<PostViewModel>>(posts));
        }

        [HttpGet("detalhes/{id:long}")]
        public async Task<IActionResult> Details(long id)
        {
            var post = await postsService.ObterPorId(id);

            if (post == null)
            {
                Notificar(Messages.RegistroNaoEncontrado);
            }

            return CustomResponse(mapper.Map<PostViewModel>(post));
        }

        [Authorize, HttpGet("novo")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize, HttpPost("novo"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            await postsService.Adicionar(mapper.Map<Post>(post));

            if (!OperacaoValida())
            {
                return CustomResponse(post);
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize, HttpGet("editar/{id:long}")]
        public async Task<IActionResult> Edit(long id)
        {
            var post = await postsService.ObterPorId(id);

            if (post == null)
            {
                Notificar(Messages.RegistroNaoEncontrado);
                return RedirectToAction("Error", "Home");
            }

            var usuarioAutorizado = userApp.IsAuthorized(post.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                Notificar(Messages.AcaoRestritaAutorOuAdmin);
                return RedirectToAction("Error", "Home");
            }

            return CustomResponse(mapper.Map<PostViewModel>(post));
        }

        [Authorize, HttpPost("editar/{id:long}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, PostViewModel postViewModel)
        {
            if (id != postViewModel.Id)
            {
                Notificar(Messages.IdsDiferentes);
                return CustomResponse(postViewModel);
            }

            if (!ModelState.IsValid) 
                return View(postViewModel);

            await postsService.Atualizar(mapper.Map<Post>(postViewModel));

            if (!OperacaoValida())
                return CustomResponse(postViewModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var post = await postsService.ObterPorId(id);

            if (post == null)
            {
                Notificar(Messages.RegistroNaoEncontrado);
                return CustomResponse();
            }

            var usuarioAutorizado = userApp.IsAuthorized(post.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                Notificar(Messages.AcaoRestritaAutorOuAdmin);
                // TODO: ControllerError
                return RedirectToAction("Error", "Home");
            }

            return View(mapper.Map<PostViewModel>(post));
        }

        [HttpPost("excluir/{id:long}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await postsService.Remover(id);

            if (!OperacaoValida())
                return CustomResponse();

            return RedirectToAction(nameof(Index));
        }
    }
}
