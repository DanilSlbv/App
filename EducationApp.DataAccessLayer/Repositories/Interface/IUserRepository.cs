using System.Collections.Generic;
using System.Threading.Tasks;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Models.Response;
using Microsoft.AspNetCore.Identity;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<ResponseModel<ApplicationUser>> GetAllUsersAsync(int page);
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<ApplicationUser> GetUserByEmailAsync(string userEmail);
        Task<List<string>> CreateAsync(ApplicationUser user, string password);
        Task<List<string>> DeleteUserAsync(string id);
        Task<List<string>> EditUserAsync(ApplicationUser user);
        Task<List<string>> PasswordRecoveryAsync(ApplicationUser applicationUser,  string newPassword);
        Task<List<string>> AddtoRoleAsync(ApplicationUser applicationUser);
        Task<string> GetRoleAsync(ApplicationUser applicationUser);
        Task<bool> CheckIsInRoleAsync(ApplicationUser applicationUser, string roleName);
        Task<bool> ConfrirmEmailAsync(string userid, string token);
        Task<bool> CheckEmailConfirmAsync(ApplicationUser user);
        Task<string> GenerateEmailConfirmTokenAsync(ApplicationUser user);
        Task<bool> SignInAsync(string userEmail,string userPassword,bool isPersitent);
        Task<bool> CanUserSigInAsync(ApplicationUser applicationUser);
        Task SignOutAsync();
    }
}
