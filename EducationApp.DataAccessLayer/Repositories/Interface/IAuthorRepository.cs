using EducationApp.DataAccessLayer.Entities;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IAuthorRepository : IBaseEFRepository<Author>
    {
        Task<Author> GetByNameAsync(string name);
    }
}
