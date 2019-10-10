﻿using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BaseModel> SigUpAsync(AccountSigUpModel userRegisterModel);
        Task<bool> ConfirmEmailAsync(string id, string token);
        Task<string> GenerateUserEmailConfrimTokenAsync(string id);
        Task<bool> CheckEmailConfirmAsync(string id);
        Task<BaseModel> SigInAsync(AccountSigInModel accountSigInModel);
        Task<BaseModel> CanSigInAsync(string userId);
        Task<string> GetRoleAsync(string userEmail);
        Task SignOutUserAsycn();
        Task<BaseModel> SendConfirmEmailAsync(string userEmail, string callbackUrl);
        Task<BaseModel> SendEmailForPasswordRecoveryAsync(string userEmail);
    }
}
