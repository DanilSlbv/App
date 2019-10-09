using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Models.Author;
using EducationApp.DataAccessLayer.Models.Pagination;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IAuthorRepository : IBaseEFRepository<Author>
    {
        Task<List<Author>> GetAllAsync();
        Task<PaginationModel<AuthorWithProductsModel>> GetAllWithProductsAsync(int page);
        Task RemoveAsync(int authorId);
    }
}
