using System.Collections.Generic;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.DataAccessLayer.Repositories.Interface;
namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModelItem>> GetAllUsersAsync();
        Task<UserModelItem> GetUserByIdAsync(string id);
        Task<UserModelItem> GetUserByEmailAsync(string userEmail);
        Task<bool> DeleteUserAsync(string id);
        Task<bool> EditUserAsync(UserEditModel userEditModel);
    }
}
