using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAcessLayer.AppContext;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class AuthorRepository: BaseEFRepository<Author>,IAuthorRepository
    {
        private readonly ApplicationContext _applicationContext;
        public AuthorRepository(ApplicationContext applicationContext):base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<Author> GetByNameAsync(string name)
        {
            var author =await  _applicationContext.Authors.FirstOrDefaultAsync(x => x.Name == name && x.IsRemoved==false);
            return author;
        }

        public async Task RemoveAsync(int authorId)
        {
            var author = await _applicationContext.Authors.FindAsync(authorId);
            author.IsRemoved = true;
            _applicationContext.Authors.Update(author);
            await _applicationContext.SaveChangesAsync();
        }
        public async Task<List<Author>> GetAllAsync()
        {
            return await _applicationContext.Authors.Where(x => x.IsRemoved == false).ToListAsync();
        }

        public async Task<string> GetAuthorNameById(int authorId)
        {
            var authorName= _applicationContext.Authors.Where(x => x.Id == authorId).Select(author=> author.Name);
            return authorName.ToString();
        }
    }
}
