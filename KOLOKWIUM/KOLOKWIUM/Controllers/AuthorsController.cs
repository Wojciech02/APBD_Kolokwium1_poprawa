using KOLOKWIUM.Repositorys;
using KOLOKWIUM.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace KOLOKWIUM.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            bool success = await _authorService.DeleteAuthorAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
