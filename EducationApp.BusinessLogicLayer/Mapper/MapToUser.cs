using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.DataAccessLayer.Entities;

namespace EducationApp.BusinessLogicLayer.Mapper
{
    public static class MapToUser
    {
        public static ApplicationUser MapToApplicationUser(UserModelItem userModelItem)
        {
            return new ApplicationUser
            {
                FirstName = userModelItem.FirstName,
                LastName = userModelItem.LastName,
                Email = userModelItem.Email,
                UserName = userModelItem.Email
            };
        }

        public static ApplicationUser MapToApplicationUser(AccountSigUpModel userModelItem)
        {
            return new ApplicationUser
            {
                FirstName = userModelItem.FirstName,
                LastName = userModelItem.LastName,
                Email = userModelItem.Email,
                UserName = userModelItem.Email
            };
        }
        public static UserModelItem MapToUserModelItem(ApplicationUser User)
        {
            return new UserModelItem
            {
                Id = User.Id,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Email = User.Email
            };
        }
    }
}
