using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAcessLayer.AppContext;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
namespace EducationApp.DataAccessLayer.Repositories
{
    public class AuthorRepository:BaseEFRepository<Author>,IAuthorRepository
    {
        public AuthorRepository(ApplicationContext _context) : base(_context)
        {

        }
        public void Update(Author author)
        {
            _dbSet.Update(author);
            _context.SaveChanges();
        }
    }
}
