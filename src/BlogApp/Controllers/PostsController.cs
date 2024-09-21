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
                                IAppIdentityUser userApp
        ) : MainController(notificador)
    {
        private readonly IPostService _postService = postsService;
        private readonly IMapper _mapper = mapper;
        private readonly IAppIdentityUser _userApp = userApp;


        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.ObterTodos();

            return View(_mapper.Map<IEnumerable<PostViewModel>>(posts));
        }

        [HttpGet("detalhes/{id:long}")]
        public async Task<IActionResult> Details(long id)
        {
            var post = await _postService.ObterPorId(id);

            if (post == null)
            {
                Notificar(Messages.RegistroNaoEncontrado);
            }

            return CustomResponse(_mapper.Map<PostViewModel>(post));
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

            var userIdLogado = _userApp.GetUserId();

            await _postService.Adicionar(_mapper.Map<Post>(post), userIdLogado);

            if (!OperacaoValida())
            {
                return CustomResponse(post);
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize, HttpGet("editar/{id:long}")]
        public async Task<IActionResult> Edit(long id)
        {
            var post = await _postService.ObterPorId(id);

            if (post == null)
            {
                Notificar(Messages.RegistroNaoEncontrado);
                return RedirectToAction("Error", "Home");
            }
            var usuarioAutorizado = _userApp.IsAuthorized(post.Autor.UsuarioId);
             
            if (!usuarioAutorizado)
            {
                Notificar(Messages.AcaoRestritaAutorOuAdmin);
                return RedirectToAction("Error", "Home");
            }
            
            return CustomResponse(_mapper.Map<PostViewModel>(post));
        }

        [Authorize, HttpPost("editar/{id:long}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, PostViewModel postViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(postViewModel);
            }

            if (id != postViewModel.Id)
            {
                Notificar(Messages.IdsDiferentes); 
                return CustomResponse(postViewModel);
            }

            await _postService.Atualizar(_mapper.Map<Post>(postViewModel));

            if (!OperacaoValida()) 
                return CustomResponse(postViewModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var post = await _postService.ObterPorId(id);

            if (post == null)
            {
                Notificar(Messages.RegistroNaoEncontrado);
                return CustomResponse();
            }
            var usuarioAutorizado = _userApp.IsAuthorized(post.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                Notificar(Messages.AcaoRestritaAutorOuAdmin);
                return RedirectToAction("Error", "Home");
            }
            return View(_mapper.Map<PostViewModel>(post));
        }

        [HttpPost("excluir/{id:long}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _postService.Remover(id);

            if (!OperacaoValida())
                return CustomResponse();

            return RedirectToAction(nameof(Index));
        }
    }
}
