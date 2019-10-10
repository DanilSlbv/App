using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.BusinessLogicLayer.Models.Response;
using System.Threading.Tasks;
using AscendingDescending = EducationApp.BusinessLogicLayer.Models.Enums.Enums.AscendingDescending;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorModelItem> GetByIdAsync(int id);
        Task<BaseModel> CreateAsync(string authorName);
        Task<BaseModel> RemoveAsync(int id);
        Task<BaseModel> EditAsync(AuthorModelItem editAuthorModelItem);
        Task<ResponseModel<AuthorWithProductsModelItem>> GetAllSortedAsync(int page, AscendingDescending ascendingDescending);
    }
}
