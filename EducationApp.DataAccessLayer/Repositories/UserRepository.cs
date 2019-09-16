using Microsoft.AspNetCore.Identity;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAccessLayer.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
         }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return  await _userManager.FindByIdAsync(id);
        }
        public async Task<ApplicationUser> GetUserByEmailAsync(string userEmail)
        {
            var applicationUser = await _userManager.FindByEmailAsync(userEmail);
            return applicationUser;
        }
        public async Task<bool> CreateAsync(ApplicationUser user, string password)
        {
           
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            ApplicationUser applicationUser = await GetUserByIdAsync(id);
            var result = await _userManager.DeleteAsync(applicationUser);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> EditUserAsync(ApplicationUser editUser)
        {
            var result = await _userManager.UpdateAsync(editUser);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> PasswordRecoveryAsync(ApplicationUser applicationUser, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(applicationUser, token, newPassword);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> AddtoRoleAsync(ApplicationUser applicationUser)
        {
            var result = await _userManager.AddToRoleAsync(applicationUser, "user");
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ConfrirmEmailAsync(string userid, string token)
        {
            ApplicationUser applicationUser = await GetUserByIdAsync(userid);
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
        public async Task<string> GenerateEmailConfirmAsync(ApplicationUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return code;
        }
        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser applicationUser)
        {
            var result = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
            return result;
        }

        public async Task<bool> SignInAsync(string userEmail, string userPassword, bool isPersitent)
        {
            var result = await _signInManager.PasswordSignInAsync(userEmail, userPassword, isPersitent, false);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
        
        public async Task<bool> CanUserSigInAsync(ApplicationUser applicationUser)
        {
            var result = await _signInManager.CanSignInAsync(applicationUser);
            return result;
        }

        public async Task ConfirmEmailAuthorizationAsync(ApplicationUser applicationUser,bool isPersitent)
        {
            await _signInManager.SignInAsync(applicationUser, isPersitent);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
