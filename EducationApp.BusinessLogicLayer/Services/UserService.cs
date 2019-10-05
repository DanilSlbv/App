using System.Collections.Generic;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Repositories.Interface;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> GetAllAsync()
        {
            var applicationUsers = await _userRepository.GetAllUsersAsync();
            var userModel = new UserModel();
            foreach (var user in applicationUsers)
            {
                userModel.Items.Add(new UserModelItem(user));
            }
            return userModel;
        }
        public async Task<UserModelItem> GetByIdAsync(string id)
        {
            if(id==null)
            {
                return null;
            }
           var userModel = new UserModelItem(await _userRepository.GetUserByIdAsync(id));
           return userModel;
        }
        public async Task<UserModelItem> GetByEmailAsync(string userEmail)
        {
            if (userEmail == null)
            {
                return null;
            }
            return new UserModelItem(await _userRepository.GetUserByEmailAsync(userEmail));
        }
        public async Task<bool> RemoveAsync(string id)
        {
            if(await _userRepository.GetUserByIdAsync(id)==null)
            {
                return false;
            }
            var result=await _userRepository.DeleteUserAsync(id);
            return result;
        }
        public async Task<bool> EditAsync(UserEditModel userEditModel)
        {
            var applicationUser = await _userRepository.GetUserByIdAsync(userEditModel.Id);
            if (applicationUser != null)
            {
                applicationUser.FirstName = userEditModel.FirstName;
                applicationUser.LastName = userEditModel.LastName;
                applicationUser.Email = userEditModel.Email;
                return await _userRepository.EditUserAsync(applicationUser);
            }
            return false;
        }
    }
}
