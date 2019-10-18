using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.DataAccessLayer.Entities;

namespace EducationApp.BusinessLogicLayer.Mapper
{
    public static class UserMapper
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

        public static ApplicationUser MapToEditApplicationUser(AccountSigUpModel userModelItem,ApplicationUser applicationUser)
        {
            applicationUser.Email = userModelItem.Email;
            applicationUser.FirstName = userModelItem.FirstName;
            applicationUser.LastName = userModelItem.LastName;
            applicationUser.EmailConfirmed = true;
            return applicationUser;
        }

        public static ApplicationUser MapToApplicationUserWithSameEmail(AccountSigUpModel userModelItem, string securityStamp)
        {
            return new ApplicationUser
            {
                FirstName = userModelItem.FirstName,
                LastName = userModelItem.LastName,
                Email = "",
                UserName = "",
                SecurityStamp = securityStamp
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
