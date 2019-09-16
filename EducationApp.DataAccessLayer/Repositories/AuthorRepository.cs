using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAcessLayer.AppContext;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class AuthorRepository:IAuthorRepository,IBaseEFRepository<Author>
    {
        private readonly ApplicationContext _context;

        public AuthorRepository(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task AddAsync(Author author)
        {
            await AddAsync(author);
        }

        public async Task DeleteAsync(string id)
        {
            await DeleteAsync(id);
        }

        public async Task EditAsync(Author author)
        {
            await EditAsync(author);
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await GetAllAsync();
        }

        public async Task<Author> GetByIdAsync(string id)
        {
            return await GetByIdAsync(id);
        }
        public async Task<Author> GetByNameAsync(string name)
        {
            var author =await  _context.Authors.FirstOrDefaultAsync(x => x.Name == name);
            return author;
        }
    }
}
