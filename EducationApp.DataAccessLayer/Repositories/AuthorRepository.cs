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
    public class AuthorRepository: BaseEFRepository<Author>,IAuthorRepository
    {
        private readonly ApplicationContext _context;
        public AuthorRepository(ApplicationContext applicationContext):base(applicationContext)
        {
            _context = applicationContext;
        }

        public async Task<Author> GetByNameAsync(string name)
        {
            var author =await  _context.Authors.FirstOrDefaultAsync(x => x.Name == name);
            return author;
        }
    }
}
