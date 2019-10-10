using Microsoft.AspNetCore.Identity;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAccessLayer.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EducationApp.DataAccessLayer.Models.Response;
using System.Linq;
using EducationApp.DataAccessLayer.Common.Constants;
using System.Collections.Generic;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<ResponseModel<ApplicationUser>> GetAllUsersAsync(int page)
        {
            var users = new ResponseModel<ApplicationUser>
            {
                Items = await _userManager.Users.Skip((page - 1) * Constants.Pagination.PageSize).Take(Constants.Pagination.PageSize).ToListAsync(),
                ItemsCount = await _userManager.Users.CountAsync()
            };
            return users;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
        public async Task<ApplicationUser> GetUserByEmailAsync(string userEmail)
        {
            var applicationUser = await _userManager.FindByEmailAsync(userEmail);
            return applicationUser;
        }
        public async Task<List<string>> CreateAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return null;
            }
            return result.Errors.Select(x=>x.Description).ToList();
        }

        public async Task<List<string>> DeleteUserAsync(string id)
        {
            ApplicationUser applicationUser = await GetUserByIdAsync(id);
            var result = await _userManager.DeleteAsync(applicationUser);
            if (result.Succeeded)
            {
                return null;
            }
            return result.Errors.Select(x => x.Description).ToList();
        }

        public async Task<List<string>> EditUserAsync(ApplicationUser editUser)
        {
            var result = await _userManager.UpdateAsync(editUser);
            if (result.Succeeded)
            {
                return null;
            }
            return result.Errors.Select(x => x.Description).ToList();
        }

        public async Task<List<string>> PasswordRecoveryAsync(ApplicationUser applicationUser, string newPassword)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
            var result = await _userManager.ResetPasswordAsync(applicationUser, token, newPassword);
            if (result.Succeeded)
            {
                return null;
            }
            return result.Errors.Select(x => x.Description).ToList();
        }

        public async Task<List<string>> AddtoRoleAsync(ApplicationUser applicationUser)
        {
            var result = await _userManager.AddToRoleAsync(applicationUser, "user");
            if (result.Succeeded)
            {
                return null;
            }
            return result.Errors.Select(x => x.Description).ToList();
        }

        public async Task<string> GetRoleAsync(ApplicationUser applicationUser)
        {
            var roles = await _userManager.GetRolesAsync(applicationUser);
            return roles.FirstOrDefault();
        }

        public async Task<bool> CheckIsInRoleAsync(ApplicationUser applicationUser, string roleName)
        {
            return await _userManager.IsInRoleAsync(applicationUser, roleName);
        }

        public async Task<bool> ConfrirmEmailAsync(string userid, string token)
        {
            var applicationUser = await GetUserByIdAsync(userid);
            var result = await _userManager.ConfirmEmailAsync(applicationUser, token);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CheckEmailConfirmAsync(ApplicationUser user)
        {
            var result = await _userManager.IsEmailConfirmedAsync(user);
            return result;
        }

        public async Task<string> GenerateEmailConfirmTokenAsync(ApplicationUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return code;
        }

        public async Task<bool> SignInAsync(string userEmail, string userPassword, bool isPersitent)
        {
            var result = await _signInManager.PasswordSignInAsync(userEmail, userPassword, isPersitent, false);
            return result.Succeeded;
        }

        public async Task<bool> CanUserSigInAsync(ApplicationUser applicationUser)
        {
            var result = await _signInManager.CanSignInAsync(applicationUser);
            return result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
