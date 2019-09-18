using EducationApp.BusinessLogicLayer.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        Task<UserModelItem> SigUpAsync(AccountSigUpModel userRegisterModel);
        Task<bool> ConfirmEmailAsync(string id, string token);
        Task<string> GeneratePasswordResetTokenAsync(string userEmail);
        Task<string> GenerateUserEmailConfrimTokenAsync(string id);
        Task<bool> CheckEmailConfirmAsync(string id);
        Task<bool> PasswordRecoveryAsync(string id, string token, string newPassword);
        Task<bool> SigInAsync(AccountSigInModel accountSigInModel);
        Task<bool> CanSigInAsync(string userId);
        Task ConfirmEmailAuthorizationAsync(string userId);
        Task<IList<string>> GetRoleAsync(string userEmail);
        Task SignOutUserAsycn();
    }
}
