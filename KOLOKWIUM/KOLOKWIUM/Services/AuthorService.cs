using KOLOKWIUM.Repositorys;
using System.Threading.Tasks;
namespace KOLOKWIUM.Services
{

    public class AuthorService
    {
        private readonly Repository _repository;

        public AuthorService(Repository repository)
        {
            _repository = repository;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            return await _repository.DeleteAuthorAsync(id);
        }
    }

}
