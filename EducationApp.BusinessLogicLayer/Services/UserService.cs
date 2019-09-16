using System.Collections.Generic;
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

        public async Task<List<UserModelItem>> GetAllAsync()
        {
            List<ApplicationUser> applicationUsers = await _userRepository.GetAllUsersAsync();
            UserModel userModel = new UserModel();
            foreach (var user in applicationUsers)
            {
                userModel.Items.Add(new UserModelItem(user));
            }
            return userModel.Items;
        }
        public async Task<UserModelItem> GetByIdAsync(string id)
        {
            UserModelItem userModel = new UserModelItem(await _userRepository.GetUserByIdAsync(id));
            return userModel;
        }
        public async Task<UserModelItem> GetByEmailAsync(string userEmail)
        {
            return new UserModelItem(await _userRepository.GetUserByEmailAsync(userEmail));
        }
        public async Task<bool> DeleteAsync(string id)
        {
            var result=await _userRepository.DeleteUserAsync(id);
            return result;
        }
        public async Task<bool> EditAsync(UserEditModel userEditModel)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                FirstName = userEditModel.FirstName,
                LastName = userEditModel.LastName,
                Email = userEditModel.Email
            };
            return await _userRepository.EditUserAsync(applicationUser);
        }
    }
}
