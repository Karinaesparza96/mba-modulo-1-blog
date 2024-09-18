using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class PostController : ControllerBase
    {
        public PostController()
        {
            
        }

        [HttpGet("todos")]
        public async Task<IActionResult> ObterTodos()
        {
            return Ok();
        }

    }
}
