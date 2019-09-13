//using EducationApp.DataAccessLayer.Entities;
//using EducationApp.DataAcessLayer.AppContext;
//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace EducationApp.DataAccessLayer.Initialization
//{
//    public class DbInitialization
//    {
//        public async static Task InitializeDb(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
//        {
//            string adminName = "dslabov1@gmail.com";
//            string password = "Qwerty-123456";
//               await roleManager.CreateAsync(new IdentityRole("admin"));
//            }
//            if (roleManager.FindByNameAsync("user") == null)
//            {
//                await roleManager.CreateAsync(new IdentityRole("user"));
//            }
//            ApplicationUser applicationUser = new ApplicationUser {Email=adminName,UserName=adminName,EmailConfirmed=true,FirstName="D",LastName="S"};
//            var result =await userManager.CreateAsync(applicationUser, password);
//            if (result != null)
//            {
//                await userManager.AddToRoleAsync(applicationUser, "admin");
//            }
//        }
//    }
//}
