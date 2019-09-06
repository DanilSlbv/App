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

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            ApplicationUser user =await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;             
            }
            return user;
        }

        public async Task<bool> SignUpAsync(ApplicationUser User,string password)
        {
            var result = await _userManager.CreateAsync(User, password);
            if(result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public async Task<bool> DeleteUserAsync(string id)
        {
            var User= await _userManager.FindByIdAsync(id);
            if (User != null)
            {
                var result = await  _userManager.DeleteAsync(User);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async  Task<bool> EditUserAsync(ApplicationUser EditUser)
        {
                var result = await _userManager.UpdateAsync(EditUser);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        public async Task<bool> AddUserRoleAsync(string RoleName)
        { 
            var result=await _roleManager.CreateAsync(new IdentityRole(RoleName));
            if(result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CheckUserRoleAsync(string Userid,string RoleName)
        {
            ApplicationUser applicationUser = await GetUserByIdAsync(Userid);
            bool result = await _userManager.IsInRoleAsync(applicationUser, RoleName);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task SignInAsync(string UserId, bool isPersitent)
        {
            ApplicationUser applicationUser = await GetUserByIdAsync(UserId);
            await _signInManager.SignInAsync(applicationUser, isPersitent);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
