using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAcessLayer.AppContext;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class AuthorRepository:BaseEFRepository<Author>,IAuthorRepository
    {
        public AuthorRepository(ApplicationContext _context) : base(_context)
        {

        }
        
    }
}
