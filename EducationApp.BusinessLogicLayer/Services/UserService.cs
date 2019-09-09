﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.BusinessLogicLayer.Helpers;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<List<UserModelItem>> GetAllUsersAsync()
        {
            List<ApplicationUser> applicationUsers = await _userRepository.GetAllAsync();
            UserModel userModel = new UserModel();
            foreach (var i in applicationUsers)
            {
                userModel.Items.Add(new UserModelItem(i));
            }
            return userModel.Items;
        }

        public async Task<UserModelItem> GetUserByIdAsync(string id)
        {
            UserModelItem userModel = new UserModelItem(await _userRepository.GetByIdAsync(id));
            return userModel;
        }
        public async Task SigUpUserAsync(UserSigUpModel userRegisterModel)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                FirstName = userRegisterModel.FirstName,
                LastName = userRegisterModel.LastName,
                Email = userRegisterModel.Email,
                Password = userRegisterModel.Password
            };
            string code = await _userRepository.GenerateEmailConfirmAsync(applicationUser);
            await _userRepository.CreateAsync(applicationUser);
            EmailHelpers emailHelpers = new EmailHelpers(applicationUser.Email, code);
            await emailHelpers.SendEmail();
            if (await _userRepository.CheckEmailConfirmAsync(applicationUser))
            {
               await _userRepository.SignInAsync(applicationUser,true);
            }
        }

        public async Task DeleteUserAsync(string id)
        {
             await _userRepository.DeleteAsync(id);
        }

        public async Task<bool> EditUserAsync(UserEditModel userEditModel)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                FirstName = userEditModel.FirstName,
                LastName = userEditModel.LastName,
                Email = userEditModel.Email
            };
            return await _userRepository.EditUserAsync(applicationUser);
        }

        public async Task<bool> AddUserRoleAsync(string roleName)
        {
           bool result= await _userRepository.AddUserRoleAsync(roleName);
            if (result)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CheckUserRoleAsync(string userId, string roleName)
        {
           bool result= await CheckUserRoleAsync(userId, roleName);
            if (result)
            {
                return true;
            }
            return false;
        }

        public async Task SigInAsync(UserSigInModel userLoginModel)
        {
            ApplicationUser applicationUser = new ApplicationUser()
            {
                Email = userLoginModel.Email,
                Password = userLoginModel.Password
            };
            if (await _userRepository.CheckEmailConfirmAsync(applicationUser))
            {
                await _userRepository.SignInAsync(applicationUser, userLoginModel.isPersitent);
            }
        }

        public async Task SignOutAsycn()
        {
            await _userRepository.SignOutAsync();
        }

    }
}
