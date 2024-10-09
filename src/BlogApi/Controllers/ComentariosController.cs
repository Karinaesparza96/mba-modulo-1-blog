using AutoMapper;
using BlogApi.DTOs;
using BlogCore.Business.Interfaces;
using BlogCore.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

[Route("api/posts/{postId:long}/comentarios")]
public class ComentariosController(IComentarioRepository comentarioRepository, 
                                   IPostRepository postRepository,
                                   IMapper mapper, IAppIdentityUser userApp) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComentarioDto>>> ObterTodosPorPostId(long postId)
    {
        var comentarios = await comentarioRepository.ObterTodosPorPost(postId);

        return Ok(mapper.Map<IEnumerable<ComentarioDto>>(comentarios));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ComentarioDto>> ObterPorId(long id, long postId)
    {
        var comentario = await comentarioRepository.ObterPorId(id, postId);

        if (comentario == null) return NotFound();

        return Ok(mapper.Map<ComentarioDto>(comentario));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody]ComentarioDto comentarioDto, long postId)
    {
        if (postId != comentarioDto.PostId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var post = await postRepository.ObterPorId(postId);

        if (post == null) return NotFound();

        await comentarioRepository.Adicionar(mapper.Map<Comentario>(comentarioDto));

        return Created();
    }

    [Authorize]
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Atualizar(long id, [FromBody]ComentarioDto comentarioDto, long postId)
    {
        if (postId != comentarioDto.PostId)
        {
            return BadRequest();
        }
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var comentario = await comentarioRepository.ObterPorId(id, postId);

        if (comentario == null) return NotFound();

        var usuarioAutorizado = userApp.IsOwnerOrAdmin(comentario.Post?.Autor.UsuarioId);

        if (!usuarioAutorizado) return Forbid();

        comentario.Conteudo = comentarioDto.Conteudo;

        await comentarioRepository.Atualizar(comentario);

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Deletar(long id, long postId)
    {
        var comentario = await comentarioRepository.ObterPorId(id, postId);

        if (comentario == null) return NotFound();

        var usuarioAutorizado = userApp.IsOwnerOrAdmin(comentario.Post?.Autor.UsuarioId);

        if (!usuarioAutorizado) return Forbid();

        await comentarioRepository.Remover(comentario);

        return NoContent();
    }
}
