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
        Task CreateUserAsync(UserRegisterModel userRegisterModel);
        Task DeleteUserAsync(string id);
        Task<bool> EditUserAsync(UserEditModel userEditModel);
        Task<bool> AddUserRoleAsync(string RoleName);
        Task<bool> CheckUserRoleAsync(string id, string RoleName);
        Task SigInAsync(UserLoginModel userLoginModel);
        Task SignOutAsycn();
      
    }
}
