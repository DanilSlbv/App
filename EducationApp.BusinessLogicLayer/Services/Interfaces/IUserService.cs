using System.Collections.Generic;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Models.Pagination;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.DataAccessLayer.Repositories.Interface;
namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<PaginationModel<UserModelItem>> GetAllAsync(int page);
        Task<UserModelItem> GetByIdAsync(string id);
        Task<UserModelItem> GetByEmailAsync(string userEmail);
        Task<bool> RemoveAsync(string id);
        Task<bool> EditAsync(UserModelItem userEditModel);
    }
}
