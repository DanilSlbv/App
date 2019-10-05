using EducationApp.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IAuthorRepository : IBaseEFRepository<Author>
    {
        Task<List<Author>> GetAllAsync();
        Task<Author> GetByNameAsync(string name);
        Task RemoveAsync(int authorId);
    }
}
