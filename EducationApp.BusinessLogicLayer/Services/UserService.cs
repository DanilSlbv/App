using System.Collections.Generic;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Common.Constants;
using EducationApp.BusinessLogicLayer.Common.Extensions;
using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.BusinessLogicLayer.Models.Response;
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

        public async Task<ResponseModel<UserModelItem>> GetAllAsync(int page)
        {
            var applicationUsers = await _userRepository.GetAllUsersAsync(page);
            var userModel = new ResponseModel<UserModelItem>();
            foreach (var user in applicationUsers.Items)
            {
                userModel.Items.Add(Mapper.MapToUser.MapToUserModelItem(user));
            }
            userModel.TotalItems = applicationUsers.ItemsCount;
            return userModel;
        }
        public async Task<UserModelItem> GetByIdAsync(string id)
        {
            if(string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
           return Mapper.MapToUser.MapToUserModelItem(await _userRepository.GetUserByIdAsync(id));
        }
        public async Task<UserModelItem> GetByEmailAsync(string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                return null;
            }
            return Mapper.MapToUser.MapToUserModelItem(await _userRepository.GetUserByEmailAsync(userEmail));
        }
        public async Task<BaseModel> RemoveAsync(string id)
        {
            var baseModel = new BaseModel();
            if(await _userRepository.GetUserByIdAsync(id)==null)
            {
                baseModel.Errors.Add(Constants.Errors.NotFount);
                return baseModel;
            }
            baseModel.Errors=await _userRepository.DeleteUserAsync(id);
            return baseModel;
        }
        public async Task<BaseModel> EditAsync(UserModelItem userEditModel)
        {
            var baseModel = new BaseModel();
            var applicationUser = await _userRepository.GetUserByIdAsync(userEditModel.Id);
            if (applicationUser != null)
            {
                baseModel.Errors.Add(Constants.Errors.NotFount);
                return baseModel;
            }
            baseModel.Errors = await _userRepository.EditUserAsync(Mapper.MapToUser.MapToApplicationUser(userEditModel));
            return baseModel;
        }
    }
}
