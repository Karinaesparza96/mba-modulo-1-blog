using System.Net;
using AutoMapper;
using BlogApi.DTOs;
using BlogCore.Business.Interfaces;
using BlogCore.Business.Messages;
using BlogCore.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

[Route("api/posts/{postId:long}/comentarios")]
public class ComentariosController(IComentarioRepository comentarioRepository, 
                                   IPostRepository postRepository,
                                   INotificador notificador,
                                   IMapper mapper, IAppIdentityUser userApp) : BaseController(notificador)
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComentarioDto>>> ObterTodosPorPostId(long postId)
    {
        var comentarios = await comentarioRepository.ObterTodosPorPost(postId);

        return CustomResponse(HttpStatusCode.OK, mapper.Map<IEnumerable<ComentarioDto>>(comentarios));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ComentarioDto>> ObterPorId(long id, long postId)
    {
        var comentario = await comentarioRepository.ObterPorId(id, postId);

        if (comentario == null)
        {
            NotificarErro(Messages.RegistroNaoEncontrado);
            return CustomResponse();
        }

        return CustomResponse(HttpStatusCode.OK, mapper.Map<ComentarioDto>(comentario));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody]ComentarioDto comentarioDto, long postId)
    {
        if (postId != comentarioDto.PostId)
        {
            NotificarErro(Messages.IdsDiferentes);
            return CustomResponse();
        }

        if (!ModelState.IsValid)
        {
            return CustomResponse(ModelState);
        }

        var post = await postRepository.ObterPorId(postId);

        if (post == null)
        {
            NotificarErro(Messages.RegistroNaoEncontrado);
            return CustomResponse();
        }

        await comentarioRepository.Adicionar(mapper.Map<Comentario>(comentarioDto));

        return CustomResponse(HttpStatusCode.Created);
    }

    [Authorize]
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Atualizar(long id, [FromBody]ComentarioDto comentarioDto, long postId)
    {
        if (postId != comentarioDto.PostId)
        {
            NotificarErro(Messages.IdsDiferentes);
            return CustomResponse();
        }
        if (!ModelState.IsValid)
        {
            return CustomResponse(ModelState);
        }

        var comentario = await comentarioRepository.ObterPorId(id, postId);

        if (comentario == null)
        {
            NotificarErro(Messages.RegistroNaoEncontrado);
            return CustomResponse();
        }

        var usuarioAutorizado = userApp.IsOwnerOrAdmin(comentario.Post?.Autor.UsuarioId);

        if (!usuarioAutorizado)
        {
            NotificarErro(Messages.AcessoNaoAutorizado);
            return CustomResponse();
        }

        comentario.Conteudo = comentarioDto.Conteudo;

        await comentarioRepository.Atualizar(comentario);

        return CustomResponse(HttpStatusCode.NoContent);
    }

    [Authorize]
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Deletar(long id, long postId)
    {
        var comentario = await comentarioRepository.ObterPorId(id, postId);

        if (comentario == null)
        {
            NotificarErro(Messages.RegistroNaoEncontrado);
            return CustomResponse();
        }

        var usuarioAutorizado = userApp.IsOwnerOrAdmin(comentario.Post?.Autor.UsuarioId);

        if (!usuarioAutorizado)
        {
            NotificarErro(Messages.AcessoNaoAutorizado);
            return CustomResponse();
        }

        await comentarioRepository.Remover(comentario);

        return CustomResponse(HttpStatusCode.NoContent);
    }
}
