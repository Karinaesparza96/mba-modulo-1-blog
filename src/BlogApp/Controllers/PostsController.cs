using AutoMapper;
using BlogApp.ViewsModels;
using BlogCore.Business.Interfaces;
using BlogCore.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    [Route("posts")]   
    
    public class PostsController(IPostService postsService, 
                                IMapper mapper, 
                                INotificador notificador,
                                IAppIdentityUser userApp) : MainController(notificador)
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

        [HttpGet("detalhes/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var post = await _postService.ObterPorId(id);

            if (post == null)
            {
                Notificar("Registro não encontrado");
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

            var userIdLogado = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _postService.Adicionar(_mapper.Map<Post>(post), userIdLogado);

            if (OperacaoValida()) return RedirectToAction(nameof(Index));

            return CustomResponse(post);
        }

        [Authorize, HttpGet("editar/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postService.ObterPorId(id);

            if (post == null)
            {
                Notificar("Registro não encontrado");
                return CustomResponse();
            }

            var usuarioAutorizado = post.Autor.UsuarioId == _userApp.GetUserId() || _userApp.IsAdmin();

            if (!usuarioAutorizado)
            {
                Notificar("A ação só pode ser realizada pelo Autor do registro ou perfil Admin.");
                return RedirectToAction("Home/Error");
            }
            
            return CustomResponse(_mapper.Map<PostViewModel>(post));
        }

        [Authorize, HttpPost("editar/{id:int}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostViewModel postViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(postViewModel);
            }

            if (id != postViewModel.Id)
            {
                Notificar("Os ids informados não são iguais."); 
                return CustomResponse(postViewModel);
            }

            var post = await _postService.ObterPorId(id);

            if (post == null)
            {
                Notificar("Registro não encontrado");
                return CustomResponse(postViewModel);
            }

            var usuarioAutorizado = post.Autor.UsuarioId == _userApp.GetUserId() || _userApp.IsAdmin();

            if (!usuarioAutorizado)
            {
                Notificar("A ação só pode ser realizada pelo Autor do registro ou perfil Admin.");
                return RedirectToAction("Home/Error");
            }

            await _postService.Atualizar(id, _mapper.Map<Post>(postViewModel));

            if (!OperacaoValida()) return CustomResponse(postViewModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postService.ObterPorId(id);

            if (post == null)
            {
                Notificar("Registro não encontrado");
                return CustomResponse();
            }

            return View(_mapper.Map<PostViewModel>(post));
        }

        [HttpPost("excluir/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _postService.ObterPorId(id);

            if (post == null)
            {
                Notificar("Registro não encontrado");
                return CustomResponse();
            }

            await _postService.Remover(id);

            if (OperacaoValida()) return RedirectToAction(nameof(Index));

            return CustomResponse();
        }
    }
}
