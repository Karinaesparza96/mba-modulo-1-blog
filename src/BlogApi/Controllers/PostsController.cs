using AutoMapper;
using BlogApi.DTOs;
using BlogCore.Business.Interfaces;
using BlogCore.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController(IPostRepository postsRepository, 
                                IMapper mapper, 
                                IAppIdentityUser userApp) : ControllerBase
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
                return NotFound();
            }

            return Ok(mapper.Map<PostDto>(post));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Adicionar(PostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            await postsRepository.Adicionar(mapper.Map<Post>(postDto));

            return Created();
        }

        [Authorize]
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Atualizar(long id, PostDto postDto)
        {
            if (id != postDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
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

            post.Titulo = postDto.Titulo;
            post.Conteudo = postDto.Conteudo;

            await postsRepository.Atualizar(post);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Deletar(long id)
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
            await postsRepository.Remover(post);

            return NoContent();
        }
    }
}
