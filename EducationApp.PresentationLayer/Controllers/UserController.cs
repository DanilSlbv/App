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

        [HttpGet("getallusers/{page}")]
        
        public async Task<IActionResult> GetAllUsers(int page)
        {
            var users = await _userService.GetAllAsync(page);
            return Ok(users);
        }

        [HttpGet("getuserbyid/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var User = await _userService.GetByIdAsync(id);
            return Ok(User);
        }

        [HttpGet("getuserbyemail/{userEmail}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetUserByEmail(string userEmail)
        {
            var User = await _userService.GetByEmailAsync(userEmail); 
            return Ok(User);
        }

        [HttpGet("edituser/{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> EditUser(string id)
        {
            
            return Ok(await _userService.GetByIdAsync(id));
        }

        [HttpPost("edituser")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> EditUser(UserModelItem userEditModel)
        {
            var result = await _userService.EditAsync(userEditModel);
            if (result)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("deleteuser")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult>  RemoveUser(string id)
        {
            var result = await _userService.RemoveAsync(id);
            if (result)
            {
                return Ok(true);
            }
            return Ok(false);
        }
    }
}
