using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interface;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class AccountService:IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModelItem> SigUpAsync(AccountSigUpModel userRegisterModel)
        {
            var applicationUser = new ApplicationUser
            {
                FirstName = userRegisterModel.FirstName,
                LastName = userRegisterModel.LastName,
                Email = userRegisterModel.Email,
                UserName = userRegisterModel.Email
            };
            bool result = await _userRepository.CreateAsync(applicationUser, userRegisterModel.Password);
            if (result)
            {
                await _userRepository.AddtoRoleAsync(applicationUser);
                return new UserModelItem(applicationUser);
            }
            return null;
        }

        public async Task<bool> ConfirmEmailAsync(string id, string token)
        {
            return await _userRepository.ConfrirmEmailAsync(id, token);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string userEmail)
        {
            var applicationUser = await _userRepository.GetUserByEmailAsync(userEmail);
            return await _userRepository.GeneratePasswordResetTokenAsync(applicationUser);
        }

        public async Task<string> GenerateUserEmailConfrimTokenAsync(string id)
        {
            var applicationUser = await _userRepository.GetUserByIdAsync(id);
            if (await _userRepository.CheckEmailConfirmAsync(applicationUser))
            {
                return null;
            }
            return await _userRepository.GenerateEmailConfirmAsync(applicationUser);
        }

        public async Task<bool> CheckEmailConfirmAsync(string id)
        {
            var applicationUser = await _userRepository.GetUserByIdAsync(id);
            bool result = await _userRepository.CheckEmailConfirmAsync(applicationUser);
            if (result)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> PasswordRecoveryAsync(string id, string token, string newPassword)
        {
            var applicationUser = await _userRepository.GetUserByIdAsync(id);
            var result = await _userRepository.PasswordRecoveryAsync(applicationUser, token, newPassword);
            return result;
        }

        public async Task<bool> SigInAsync(AccountSigInModel accountSigInModel)
        {
            var applicationUser = await _userRepository.GetUserByEmailAsync(accountSigInModel.Email);
            var checkConfirm = await CheckEmailConfirmAsync(applicationUser.Id);
            if (checkConfirm)
            {
                var resultSigIn = await _userRepository.SignInAsync(accountSigInModel.Email, accountSigInModel.Password, accountSigInModel.isPersitent);
                return resultSigIn;
            }
            return checkConfirm;
        }

        public async Task<bool> CanSigInAsync(string userId)
        {
            var applicationUser = await _userRepository.GetUserByIdAsync(userId);
            var resultCheck = await _userRepository.CanUserSigInAsync(applicationUser);
            return resultCheck;
        }

        public async Task ConfirmEmailAuthorizationAsync(string userId)
        {
            var applicationUser = await _userRepository.GetUserByIdAsync(userId);
            await _userRepository.ConfirmEmailAuthorizationAsync(applicationUser, true);
        }

        public async Task SignOutUserAsycn()
        {
            await _userRepository.SignOutAsync();
        }
    }
}
