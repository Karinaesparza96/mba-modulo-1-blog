using System.Net;
using AutoMapper;
using BlogApi.DTOs;
using BlogCore.Business.Interfaces;
using BlogCore.Business.Messages;
using BlogCore.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController(IPostRepository postsRepository, 
                                IMapper mapper, 
                                INotificador notificador,
                                IAppIdentityUser userApp) : BaseController(notificador)
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> ObterTodos()
        {
            var posts = await postsRepository.ObterTodos();
            return Ok(mapper.Map<IEnumerable<PostDto>>(posts));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<PostDto>> ObterPorId(long id)
        {
            var post = await postsRepository.ObterPorId(id);

            if (post == null)
            {   
                NotificarErro(Messages.RegistroNaoEncontrado);
                return CustomResponse();
            }

            return CustomResponse(HttpStatusCode.OK, mapper.Map<PostDto>(post));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Adicionar(PostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            await postsRepository.Adicionar(mapper.Map<Post>(postDto));

            return CustomResponse(HttpStatusCode.Created);
        }

        [Authorize]
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Atualizar(long id, PostDto postDto)
        {
            if (id != postDto.Id)
            {
                NotificarErro(Messages.IdsDiferentes);
                return CustomResponse();
            }

            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var post = await postsRepository.ObterPorId(id);

            if (post == null)
            {   
                NotificarErro(Messages.RegistroNaoEncontrado);
                return CustomResponse();
            }

            var usuarioAutorizado = userApp.IsOwnerOrAdmin(post.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {   
                NotificarErro(Messages.AcessoNaoAutorizado);
                return CustomResponse();
            }

            post.Titulo = postDto.Titulo;
            post.Conteudo = postDto.Conteudo;

            await postsRepository.Atualizar(post);

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [Authorize]
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Deletar(long id)
        {
            var post = await postsRepository.ObterPorId(id);

            if (post == null)
            {
                NotificarErro(Messages.RegistroNaoEncontrado);
                return CustomResponse();
            }
            var usuarioAutorizado = userApp.IsOwnerOrAdmin(post.Autor.UsuarioId);

            if (!usuarioAutorizado)
            {
                NotificarErro(Messages.AcessoNaoAutorizado);
                return CustomResponse();
            }
            await postsRepository.Remover(post);

            return CustomResponse(HttpStatusCode.NoContent);
        }
    }
}
