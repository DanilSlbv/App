using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EducationApp.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IUserRepository:IBaseEFRepository<ApplicationUser>
    {
        Task<bool> EditUserAsync(ApplicationUser user);
        Task<bool> AddUserRoleAsync(string roleName);
        Task<bool> CheckUserRoleAsync(string id,string roleName);
        Task<bool> CheckEmailConfirmAsync(ApplicationUser user);
        Task<string> GenerateEmailConfirmAsync(ApplicationUser user);
        Task<bool> SignInAsync(ApplicationUser user,bool isPersitent);
        Task SignOutAsync();
    }
}
