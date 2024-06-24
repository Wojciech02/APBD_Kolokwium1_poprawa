using KOLOKWIUM.DTOs;
using KOLOKWIUM.Services;
using KOLOKWIUM.Repositorys;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace KOLOKWIUM.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class LibrariesController : ControllerBase
    {
        private readonly LibraryService _libraryService;

        public LibrariesController(LibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryDto>> GetLibrary(int id)
        {
            var library = await _libraryService.GetLibraryAsync(id);

            if (library == null)
            {
                return NotFound();
            }

            return Ok(library);
        }
    }

}
