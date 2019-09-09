using Microsoft.AspNetCore.Identity;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAccessLayer.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            var user =await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;             
            }
            return user;
        }

        public async Task CreateAsync(ApplicationUser user)
        {
            await _userManager.CreateAsync(user, user.Password);
        }
        
        public async Task DeleteAsync(string id)
        {
            ApplicationUser applicationUser = await GetByIdAsync(id);
            await _userManager.DeleteAsync(applicationUser);
        }

        public async  Task<bool> EditUserAsync(ApplicationUser editUser)
        {
                var result = await _userManager.UpdateAsync(editUser);
                if (result.Succeeded)
                {
                    return true;
                }
                return false;
        }

        public async Task<bool> AddUserRoleAsync(string roleName)
        { 
            var result=await _roleManager.CreateAsync(new IdentityRole(roleName));
            if(result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CheckUserRoleAsync(string userid,string roleName)
        {
            ApplicationUser applicationUser = await GetByIdAsync(userid);
            bool result = await _userManager.IsInRoleAsync(applicationUser, roleName);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task<bool>CheckEmailConfirmAsync(ApplicationUser user)
        {
            var result = await _userManager.IsEmailConfirmedAsync(user);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task<string> GenerateEmailConfirmAsync(ApplicationUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return code;
        }

        public async Task<bool> SignInAsync(ApplicationUser User, bool isPersitent)
        {
            var result = await _signInManager.PasswordSignInAsync(User.Email, User.Password, isPersitent, false);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(User, isPersitent);
                return true;
            }
            return false;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
