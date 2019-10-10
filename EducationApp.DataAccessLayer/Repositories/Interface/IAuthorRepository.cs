using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Models.Author;
using EducationApp.DataAccessLayer.Models.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AscendingDescending = EducationApp.DataAccessLayer.Entities.Enums.Enums.AscendingDescending;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IAuthorRepository : IBaseEFRepository<Author>
    {
        Task<List<Author>> GetAllAsync();
        Task<ResponseModel<AuthorWithProductsModel>> GetAllWithProductsAsync(int page,AscendingDescending sortById);
    }
}
