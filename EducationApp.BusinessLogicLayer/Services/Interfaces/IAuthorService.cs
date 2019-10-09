using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Models.Pagination;
using System.Threading.Tasks;
using AscendingDescending = EducationApp.BusinessLogicLayer.Models.Enums.Enums.AscendingDescending;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IAuthorService
    {
       // Task<AuthorModel> GetAllAsync();
        Task<AuthorModelItem> GetByIdAsync(int id);
        Task<bool> AddAsync(string authorName);
        Task<bool> RemoveAsync(int id);
        Task<bool> EditAsync(AuthorModelItem editAuthorModelItem);
        Task<PaginationModel<AuthorWithProductsModelItem>> GetAllSortedAsync(int page, AscendingDescending ascendingDescending);
    }
}
