using BlogApp.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace BlogApp.Controllers
{
    public class HomeController(ILogger<HomeController> logger) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Error/{statusCode}")]
        public IActionResult Error(HttpStatusCode statusCode)
        {
            var viewModel = new ErrorViewModel
            {
                Title = ((int)statusCode).ToString(),
                Message = statusCode.ToString()
            };

            return View(viewModel);
        }
    }
}
