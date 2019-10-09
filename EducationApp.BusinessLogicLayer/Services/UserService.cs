using System.Collections.Generic;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Models.Pagination;
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

        public async Task<PaginationModel<UserModelItem>> GetAllAsync(int page)
        {
            var applicationUsers = await _userRepository.GetAllUsersAsync(page);
            var userModel = new PaginationModel<UserModelItem>();
            foreach (var user in applicationUsers.Items)
            {
                userModel.Items.Add(new UserModelItem(user));
            }
            userModel.TotalItems = applicationUsers.ItemsCount;
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
        public async Task<bool> EditAsync(UserModelItem userEditModel)
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
