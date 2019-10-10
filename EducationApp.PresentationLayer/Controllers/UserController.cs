using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using EducationApp.PresentationLayer.Common.Constants;

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

        [HttpGet("getall/{page}")]
        [Authorize(Roles = Constants.Roles.AdmimRole)]
        public async Task<IActionResult> GetAllUsers(int page)
        {
            var users = await _userService.GetAllAsync(page);
            return Ok(users);
        }

        [HttpGet("getbyid/{id}")]
        [Authorize(Roles = Constants.Roles.AdmimRole)]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }

        [HttpGet("getbyemail/{userEmail}")]
        [Authorize(Roles = Constants.Roles.AdmimRole)]
        public async Task<IActionResult> GetUserByName(string userName)
        {
            var user = await _userService.GetByEmailAsync(userName); 
            return Ok(user);
        }
        
        [HttpPost("edit")]
        [Authorize(Roles = Constants.Roles.AllRoles)]
        public async Task<IActionResult> EditUser(UserModelItem userEditModel)
        {
            var result = await _userService.EditAsync(userEditModel);
            return Ok(result);
        }

        [HttpGet("remove/{userId}")]
        [Authorize(Roles = Constants.Roles.AdmimRole)]
        public async Task<IActionResult>  RemoveUser(string id)
        {
            var result = await _userService.RemoveAsync(id);
            return Ok(result);
        }
    }
}
