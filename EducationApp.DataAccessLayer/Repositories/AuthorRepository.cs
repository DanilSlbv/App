using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAcessLayer.AppContext;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class AuthorRepository:IAuthorRepository
    {
        private readonly IBaseEFRepository<Author> _baseEFRepository;
        private readonly ApplicationContext _context;

        public AuthorRepository(ApplicationContext applicationContext, IBaseEFRepository<Author> baseEFRepository)
        {
            _context = applicationContext;
            _baseEFRepository = baseEFRepository;
        }

        public async Task AddItemAsync(Author author)
        {
            await _baseEFRepository.AddItemAsync(author);
        }

        public async Task DeleteItemAsync(string id)
        {
            await _baseEFRepository.DeleteItemAsync(id);
        }

        public async Task EditItemAsync(Author author)
        {
            await _baseEFRepository.EditItemAsync(author);
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _baseEFRepository.GetAllAsync();
        }

        public async Task<Author> GetByIdAsync(string id)
        {
            return await _baseEFRepository.GetByIdAsync(id);
        }
        public async Task<Author> GetByNameAsync(string name)
        {
            var author =  _context.Authors.FirstOrDefault(x => x.Name == name);
            return author;
        }
    }
}
