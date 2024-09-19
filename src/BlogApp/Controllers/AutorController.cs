using AutoMapper;
using BlogApp.ViewsModels;
using BlogCore.Business.Interfaces;
using BlogCore.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    [Route("autores")]
    public class AutorController(INotificador notificador, 
                                IAutorService autorService, 
                                IMapper mapper,
                                IAppIdentityUser userApp) : MainController(notificador)
    {
        private readonly IAutorService _autorService = autorService;
        private readonly IMapper _mapper = mapper;
        private readonly IAppIdentityUser _user = userApp;

        public IActionResult Index()
        {
            return View();
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
                return View(autorViewModel);
            }
            
            var autor = _mapper.Map<Autor>(autorViewModel);
            await _autorService.Adicionar(autor, _user.GetUserId());

            if (!OperacaoValida()) return CustomResponse(autorViewModel);

            return RedirectToAction("Posts", "Index");
        }

        [Authorize, HttpGet("editar/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var autor = await _autorService.ObterPorId(id);

            if (autor == null)
            {
                Notificar("Registro não encontrado.");
                return CustomResponse(autor);
            }

            var usuarioAutorizado = autor.UsuarioId == _user.GetUserId() || _user.IsAdmin();

            if (!usuarioAutorizado)
            {
                TempData["Messages"] = "Registro pertence a outro usuário.";
                return RedirectToAction("Unauthorized", "Error");
            }

            return View();
        }

        [Authorize, HttpPost("editar/{id:int}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AutorViewModel autorViewModel)
        {   
            if (!ModelState.IsValid)
            {
                return View(autorViewModel);
            }

            if (id != autorViewModel.Id)
            {
                ModelState.AddModelError(string.Empty, "Os ids informados não são iguais.");
                return View(autorViewModel);
            }

            var autor = await _autorService.ObterPorId(id);

            if (autor == null)
            {
                Notificar("Registro não encontrado.");
                return CustomResponse(autor);
            }

            var usuarioAutorizado = autor.UsuarioId == _user.GetUserId() || _user.IsAdmin();

            if (!usuarioAutorizado)
            {
                TempData["Messages"] = "Registro pertence a outro usuário.";
                return RedirectToAction("Unauthorized", "Error");
            }

            await _autorService.Atualizar(_mapper.Map<Autor>(autor), _user.GetUserId());

            return View();
        }

       
    }
}
