using System.Collections.Generic;
using System.Threading.Tasks;
using EducationApp.DataAccessLayer.Entities;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<ApplicationUser> GetUserByEmailAsync(string userEmail);
        Task<bool> CreateAsync(ApplicationUser user, string password);
        Task<bool> DeleteUserAsync(string id);
        Task<bool> EditUserAsync(ApplicationUser user);
        Task<bool> PasswordRecoveryAsync(ApplicationUser applicationUser, string token, string newPassword);
        Task<bool> AddtoRoleAsync(ApplicationUser applicationUser);
        Task<IList<string>> GetRoleAsync(ApplicationUser applicationUser);
        Task<bool> CheckIsInRoleAsync(ApplicationUser applicationUser, string roleName);
        Task<bool> ConfrirmEmailAsync(string userid, string token);
        Task<bool> CheckEmailConfirmAsync(ApplicationUser user);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser applicationUser);
        Task<string> GenerateEmailConfirmAsync(ApplicationUser user);
        Task<bool> SignInAsync(string userEmail,string userPassword,bool isPersitent);
        Task<bool> CanUserSigInAsync(ApplicationUser applicationUser);
        Task ConfirmEmailAuthorizationAsync(ApplicationUser applicationUser, bool isPersitent);
        Task SignOutAsync();
    }
}
