using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EducationApp.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<bool> SignUpAsync(ApplicationUser User,string password);
        Task<bool> EditUserAsync(ApplicationUser User);
        Task<bool> DeleteUserAsync(string id);
        Task<bool> AddUserRoleAsync(string RoleName);
        Task<bool> CheckUserRoleAsync(string id,string RoleName);
        Task SignInAsync(ApplicationUser User,bool isPersitent);
        Task SignOutAsync();
        Task SignInAsync(string userId, bool isPersient);
    }
}
