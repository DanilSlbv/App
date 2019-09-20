using System.Collections.Generic;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.DataAccessLayer.Repositories.Interface;
namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModelItem>> GetAllAsync();
        Task<UserModelItem> GetByIdAsync(string id);
        Task<UserModelItem> GetByEmailAsync(string userEmail);
        Task<bool> DeleteAsync(string id);
        Task<bool> EditAsync(UserEditModel userEditModel);
    }
}
