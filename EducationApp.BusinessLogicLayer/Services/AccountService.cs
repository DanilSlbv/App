using EducationApp.BusinessLogicLayer.Common.Constants;
using EducationApp.BusinessLogicLayer.Helpers;
using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interface;
using Microsoft.Extensions.Configuration;
using PasswordGenerator;
using System.Linq;
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

        public async Task<BaseModel> SigUpAsync(AccountSigUpModel userRegisterModel)
        {
            var baseModel = new BaseModel();
            var applicationUser = Mapper.MapToUser.MapToApplicationUser(userRegisterModel);
            var result = await _userRepository.CreateAsync(applicationUser, userRegisterModel.Password);
            baseModel.Errors = result;
            return baseModel;
        }

        public async Task<bool> ConfirmEmailAsync(string id, string token)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(token))
            {
                return false;
            }
            return await _userRepository.ConfrirmEmailAsync(id, token);
        }


        public async Task<string> GenerateUserEmailConfrimTokenAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            var applicationUser = await _userRepository.GetUserByIdAsync(id);
            if (applicationUser == null)
            {
                return Constants.Errors.NotFount;
            }
            if (await _userRepository.CheckEmailConfirmAsync(applicationUser))
            {
                return null;
            }
            return await _userRepository.GenerateEmailConfirmTokenAsync(applicationUser);
        }

        public async Task<bool> CheckEmailConfirmAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }
            var applicationUser = await _userRepository.GetUserByIdAsync(id);
            return await _userRepository.CheckEmailConfirmAsync(applicationUser); 
        }

        public async Task<BaseModel> SigInAsync(AccountSigInModel accountSigInModel)
        {
            var baseModel = new BaseModel();
            var applicationUser = await _userRepository.GetUserByEmailAsync(accountSigInModel.Email);
            if (applicationUser == null)
            {
                baseModel.Errors.Add(Constants.Errors.SigInWrongForm);
                return baseModel;
            }
            var checkConfirm = await CheckEmailConfirmAsync(applicationUser.Id);
            if (!checkConfirm)
            {
                baseModel.Errors.Add(Constants.Errors.ConfirmEmailError);
                return baseModel;
            }
            if(await _userRepository.SignInAsync(accountSigInModel.Email, accountSigInModel.Password, accountSigInModel.isPersitent))
            {
                return null;
            }
            baseModel.Errors.Add(Constants.Errors.SigInError);
            return baseModel;
        }

        public async Task<BaseModel> CanSigInAsync(string userId)
        {
            var baseModel = new BaseModel();
            if (string.IsNullOrWhiteSpace(userId))
            {
                baseModel.Errors.Add(Constants.Errors.NotFount);
                return baseModel;
            }
            var applicationUser = await _userRepository.GetUserByIdAsync(userId);
            if(await _userRepository.CanUserSigInAsync(applicationUser))
            {
                return baseModel;
            }
            baseModel.Errors.Add(Constants.Errors.SigInError);
            return baseModel;
        }

        public async Task<string> GetRoleAsync(string userEmail)
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

        public async Task<BaseModel> SendConfirmEmailAsync(string userEmail,string callbackUrl)
        {
            var baseModel = new BaseModel();
            var emailHelpers = new EmailHelpers(userEmail);
            var mail = emailHelpers.MailMessageForEmailConfirm(callbackUrl);
            return await emailHelpers.SendEmailAsync(mail);
        }
        public async Task<BaseModel> SendEmailForPasswordRecoveryAsync(string userEmail)
        {
            var baseModel = new BaseModel();
            var applicationUser = await _userRepository.GetUserByEmailAsync(userEmail);
            if (applicationUser == null)
            {
                baseModel.Errors.Add(Constants.Errors.NotFount);
                return baseModel;
            }
            string newPassword = GenerateNewPassword();
            var recoveryResult = await RecoveryPassword(applicationUser, newPassword);
            if (!recoveryResult.Errors.Count().Equals(0))
            {
                baseModel = recoveryResult;
                return baseModel;
            }
            var emailHelpers = new EmailHelpers(userEmail);
            var mail = emailHelpers.MailMessageForPasswordRecovery(newPassword);
            return await emailHelpers.SendEmailAsync(mail);
        }

        private async Task<BaseModel> RecoveryPassword(ApplicationUser applicationUser,string newPassword)
        {
            var baseModel = new BaseModel();
            baseModel.Errors=await _userRepository.PasswordRecoveryAsync(applicationUser, newPassword);
            return baseModel;
        }

        private string GenerateNewPassword()
        {
            var generate = new Password().IncludeLowercase().IncludeUppercase().IncludeSpecial().IncludeNumeric().LengthRequired(8);
            return generate.Next();
        }
    }
}
