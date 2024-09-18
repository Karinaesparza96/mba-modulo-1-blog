using AutoMapper;
using BlogApp.ViewsModels;
using BlogCore.Business.Interfaces;
using BlogCore.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    [Route("autores")]
    public class AutorController(INotificador notificador, IAutorService autorService, IMapper mapper) : MainController(notificador)
    {   
        private readonly IAutorService _autorService = autorService;
        private readonly IMapper _mapper = mapper;

        public IActionResult Index()
        {
            return RedirectToRoute("posts");
        }

        [Authorize, HttpGet("novo")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize, HttpPost("novo")]
        public async Task<IActionResult> Create(AutorViewModel autorViewModel)
        {   
            if (!ModelState.IsValid)
            {
                return CustomResponse(autorViewModel);
            }
            var autor = _mapper.Map<Autor>(autorViewModel);
            var userIdLogado = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _autorService.Criar(autor, userIdLogado);

            if (OperacaoValida()) return RedirectToAction(nameof(Index));

            return CustomResponse(autorViewModel);
        }
    }
}
