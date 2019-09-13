using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Controllers
{
    [Route("user")]
    [ApiController]
    [Authorize]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Authorize(Roles = "user")]
        [Route("getallusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var Users = await _userService.GetAllUsersAsync();
            return Ok(User);
        }
        [HttpGet]
        [Authorize(Roles = "user")]
        [Route("getuserbyid/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var User = await _userService.GetUserByIdAsync(id);
            return Ok(User);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("getuserbyemail/{userEmail}")]
        public async Task<IActionResult> GetUserByEmail(string userEmail)
        {
            var User = await _userService.GetUserByEmailAsync(userEmail);
            return Ok(User);
        }
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        [Route("edituser/{id}")]
        public async Task<IActionResult> EditUser(string id)
        {
            return Ok(await _userService.GetUserByIdAsync(id));
        }
        [HttpPost]

        [Authorize(Roles = "admin,user")]
        [Route("edituser")]
        public async Task<IActionResult> EditUser(UserEditModel userEditModel)
        {
            var result = await _userService.EditUserAsync(userEditModel);
            if (result)
            {
                return Content("UserEdit--Success");
            }
            return Content("UserEdit--Error");
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("deleteuser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result)
            {
                return Content("UserDelete-Success");
            }
            return Content("UserDelete---Error");
        }
    }
}
