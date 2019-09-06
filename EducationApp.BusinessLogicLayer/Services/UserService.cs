using System.Collections.Generic;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAccessLayer.Entities;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            List<ApplicationUser> applicationUsers = await _userRepository.GetAllUsersAsync();
            List<UserModel> usersModel = null;

            foreach (var i in applicationUsers)
            {
                usersModel.Add(new UserModel { Email = i.Email, FirstName = i.FirstName, LastName = i.LastName });
            }
            return usersModel;
        }

        public async Task<UserModel> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            UserModel userModel = new UserModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            return userModel;
        }
        public async Task<bool> SignUpAsync(UserRegisterModel userRegisterModel)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                FirstName = userRegisterModel.FirstName,
                LastName = userRegisterModel.LastName,
                Email = userRegisterModel.Email,
                Password = userRegisterModel.Password
            };
            bool result = await _userRepository.SignUpAsync(applicationUser, applicationUser.Password);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            return await _userRepository.DeleteUserAsync(id);
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

        public async Task<bool> AddUserRoleAsync(string RoleName)
        {
           bool result= await _userRepository.AddUserRoleAsync(RoleName);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CheckUserRoleAsync(string Userid, string RoleName)
        {
           bool result= await CheckUserRoleAsync(Userid, RoleName);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task SigInAsync(string UserId,bool IsPersient)
        {
            await _userRepository.SignInAsync(UserId, IsPersient);
        }

        public async Task SignOutAsycn()
        {
            await _userRepository.SignOutAsync();
        }

    }
}
