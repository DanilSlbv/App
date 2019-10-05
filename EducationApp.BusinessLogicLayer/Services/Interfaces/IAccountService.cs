using EducationApp.BusinessLogicLayer.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> SigUpAsync(AccountSigUpModel userRegisterModel);
        Task<bool> ConfirmEmailAsync(string id, string token);
        Task<string> GenerateUserEmailConfrimTokenAsync(string id);
        Task<bool> CheckEmailConfirmAsync(string id);
        Task<string> PasswordRecoveryAsync(string userEmail);
        Task<bool> SigInAsync(AccountSigInModel accountSigInModel);
        Task<bool> CanSigInAsync(string userId);
        Task ConfirmEmailAuthorizationAsync(string userId);
        Task<IList<string>> GetRoleAsync(string userEmail);
        Task SignOutUserAsycn();
    }
}
