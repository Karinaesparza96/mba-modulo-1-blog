using AutoMapper;
using BlogApp.ViewsModels;
using BlogCore.Business.Interfaces;
using BlogCore.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    [Route("posts")]
    public class PostsController(IPostRepository postsRepository, 
                                IMapper mapper,
                                IAppIdentityUser userApp) : BaseController(userApp)
    {
        private readonly IAppIdentityUser _userApp = userApp;

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var posts = await postsRepository.ObterTodos();
            var postsViewModel = mapper.Map<IEnumerable<PostViewModel>>(posts);

            postsViewModel.DefinirPermissoes(_userApp);

            return View(postsViewModel);
        }

        [HttpGet("detalhes/{id:long}")]
        public async Task<IActionResult> Details(long id)
        {
            var post = await postsRepository.ObterPorId(id);

            if (post == null)
            {
                return NotFound();
            }
            
            var postViewModel = mapper.Map<PostViewModel>(post);
            postViewModel.DefinirPermissao(_userApp);

            return View(postViewModel);
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

            await postsRepository.Adicionar(mapper.Map<Post>(post));

            return RedirectToAction("Index");
        }

        [Authorize, HttpGet("editar/{id:long}")]
        public async Task<IActionResult> Edit(long id)
        {
            var post = await postsRepository.ObterPorId(id);

            if (post == null)
            {
                return NotFound();
            }

            var usuarioAutorizado = IsOwnerOrAdmin(post.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                return RedirectPageErrorForbidden();
            }

            var postViewModel = mapper.Map<PostViewModel>(post);
            postViewModel.DefinirPermissao(_userApp);

            return View(postViewModel);
        }

        [Authorize, HttpPost("editar/{id:long}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, PostViewModel postViewModel)
        {
            if (id != postViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(postViewModel);
            }

            var post = await postsRepository.ObterPorId(id);

            if (post == null)
            {
                return NotFound();
            }

            var usuarioAutorizado = IsOwnerOrAdmin(post.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                return RedirectPageErrorForbidden();
            }

            post.Titulo = postViewModel.Titulo;
            post.Conteudo = postViewModel.Conteudo;

            await postsRepository.Atualizar(post);

            return RedirectToAction("Index");
        }

        [Authorize, HttpGet("excluir/{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var post = await postsRepository.ObterPorId(id);

            if (post == null)
            {
                return NotFound();
            }

            var usuarioAutorizado = IsOwnerOrAdmin(post.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                return RedirectPageErrorForbidden();
            }

            var postViewModel = mapper.Map<PostViewModel>(post);
            postViewModel.DefinirPermissao(_userApp);

            return View(postViewModel);
        }

        [Authorize, HttpPost("excluir/{id:long}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var post = await postsRepository.ObterPorId(id);

            if (post == null)
            {
                return NotFound();
            }
            var usuarioAutorizado = IsOwnerOrAdmin(post.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                return RedirectPageErrorForbidden();
            }
            await postsRepository.Remover(post);

            return RedirectToAction("Index");
        }
    }
}
