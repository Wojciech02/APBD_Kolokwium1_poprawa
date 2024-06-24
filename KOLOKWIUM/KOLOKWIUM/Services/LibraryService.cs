using KOLOKWIUM.DTOs;
using KOLOKWIUM.Repositorys;
using System.Threading.Tasks;
namespace KOLOKWIUM.Services
{


    public class LibraryService
    {
        private readonly Repository _repository;

        public LibraryService(Repository repository)
        {
            _repository = repository;
        }

        public async Task<LibraryDto> GetLibraryAsync(int id)
        {
            return await _repository.GetLibraryAsync(id);
        }
    }

}
