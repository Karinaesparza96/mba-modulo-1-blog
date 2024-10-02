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
    [Route("posts")]
    public class PostsController(IPostRepository postsRepository, 
                                IMapper mapper,
                                IAppIdentityUser userApp) : Controller
    {
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var posts = await postsRepository.ObterTodos();

            return View(mapper.Map<IEnumerable<PostViewModel>>(posts));
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

            postViewModel.Comentarios?.DefinirPermissoes(userApp, post.Autor.UsuarioId);

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

            var usuarioAutorizado = userApp.IsOwnerOrAdmin(post.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                return Forbid();
            }

            return View(mapper.Map<PostViewModel>(post));
        }

        [Authorize, HttpPost("editar/{id:long}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, PostViewModel postViewModel)
        {
            if (id != postViewModel.Id)
            {
                ModelState.AddModelError("", Messages.IdsDiferentes);
                return View(postViewModel);
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

            var usuarioAutorizado = userApp.IsOwnerOrAdmin(post.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                return Forbid();
            }

            post.Titulo = postViewModel.Titulo;
            post.Conteudo = postViewModel.Conteudo;

            await postsRepository.Atualizar(post);

            return RedirectToAction("Index");
        }

        [HttpGet("excluir/{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var post = await postsRepository.ObterPorId(id);

            if (post == null)
            {
                NotFound();
            }

            var usuarioAutorizado = userApp.IsOwnerOrAdmin(post?.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                Forbid();
            }

            return View(mapper.Map<PostViewModel>(post));
        }

        [HttpPost("excluir/{id:long}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var post = await postsRepository.ObterPorId(id);

            if (post == null)
            {
                return NotFound();
            }
            var usuarioAutorizado = userApp.IsOwnerOrAdmin(post.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                NotFound();
            }
            await postsRepository.Remover(post);

            return RedirectToAction("Index");
        }
    }
}
