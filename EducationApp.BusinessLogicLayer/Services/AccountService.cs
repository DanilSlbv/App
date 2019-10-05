using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interface;
using PasswordGenerator;
using System.Collections.Generic;
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


        public async Task<bool> SigUpAsync(AccountSigUpModel userRegisterModel)
        {
            if (userRegisterModel == null)
            {
                return false;
            }
            var applicationUser = new ApplicationUser
            {
                FirstName = userRegisterModel.FirstName,
                LastName = userRegisterModel.LastName,
                Email = userRegisterModel.Email,
                UserName = userRegisterModel.Email
            };
            var result = await _userRepository.CreateAsync(applicationUser, userRegisterModel.Password);
            if (result)
            {
                await _userRepository.AddtoRoleAsync(applicationUser);
                return result;
            }
            return false;
        }

        public async Task<bool> ConfirmEmailAsync(string id, string token)
        {
            if (id == null || token == null)
            {
                return false;
            }
            return await _userRepository.ConfrirmEmailAsync(id, token);
        }


        public async Task<string> GenerateUserEmailConfrimTokenAsync(string id)
        {
            if (id == null)
            {
                return null;
            }
            var applicationUser = await _userRepository.GetUserByIdAsync(id);
            if (await _userRepository.CheckEmailConfirmAsync(applicationUser))
            {
                return null;
            }
            return await _userRepository.GenerateEmailConfirmAsync(applicationUser);
        }

        public async Task<bool> CheckEmailConfirmAsync(string id)
        {
            if (id == null)
            {
                return false;
            }
            var applicationUser = await _userRepository.GetUserByIdAsync(id);
            return await _userRepository.CheckEmailConfirmAsync(applicationUser); 
        }

        public async Task<string> PasswordRecoveryAsync(string userEmail)
        {   
            var generage = new Password().IncludeLowercase().IncludeUppercase().IncludeSpecial().IncludeNumeric().LengthRequired(8);
            var newPassword = generage.Next();
            var applicationUser = await _userRepository.GetUserByEmailAsync(userEmail);
            await _userRepository.PasswordRecoveryAsync(applicationUser, newPassword);
            return newPassword;
        }

        public async Task<bool> SigInAsync(AccountSigInModel accountSigInModel)
        {
            if (accountSigInModel == null)
            {
                return false;
            }
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
            if (userId == null)
            {
                return false;
            }
            var applicationUser = await _userRepository.GetUserByIdAsync(userId);
            return await _userRepository.CanUserSigInAsync(applicationUser);
        }

        public async Task ConfirmEmailAuthorizationAsync(string userId)
        {
            var applicationUser = await _userRepository.GetUserByIdAsync(userId);
            await _userRepository.ConfirmEmailAuthorizationAsync(applicationUser, true);
        }

        public async Task<IList<string>> GetRoleAsync(string userEmail)
        {
            var applicationUser = await _userRepository.GetUserByEmailAsync(userEmail);
            if (applicationUser == null)
            {
                return null;
            }
           return await _userRepository.GetRoleAsync(applicationUser);
        }
        
        public async Task SignOutUserAsycn()
        {
            await _userRepository.SignOutAsync();
        }
    }
}
