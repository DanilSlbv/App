using EducationApp.DataAccessLayer.Common.Constants;
using EducationApp.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Initialization
{
    public class DbInitialize
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public DbInitialize(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task InitializeAdmin()
        {
            await CreateRoles();
            string password = "Qwerty-12345";
            var admin = new ApplicationUser()
            {
                FirstName = "Danil",
                LastName="Slabov",
                Email="dslabov1@gmail.com",
                EmailConfirmed=true
            };
            await _userManager.CreateAsync(admin,password);
            await _userManager.AddToRoleAsync(admin, Constants.Roles.AdminRole);
        }
        private async Task CreateRoles()
        {
            var adminRole = new IdentityRole(Constants.Roles.AdminRole);
            var userRole = new IdentityRole(Constants.Roles.UserRole);
            await _roleManager.CreateAsync(adminRole);
            await _roleManager.CreateAsync(userRole);
        }
    }
}

   


