using System.Collections.Generic;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.DataAccessLayer.Repositories.Interface;
namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByIdAsync(string id);
        Task<bool> SignUpAsync(UserRegisterModel userRegisterModel);
        Task<bool> DeleteUserAsync(string id);
        Task<bool> EditUserAsync(UserEditModel userEditModel);
        Task<bool> AddUserRoleAsync(string RoleName);
        Task<bool> CheckUserRoleAsync(string id, string RoleName);
        Task SigInAsync();
        Task SignOutAsycn();
      
    }
}
