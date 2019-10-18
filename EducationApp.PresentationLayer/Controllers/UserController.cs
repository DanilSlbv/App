using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using EducationApp.PresentationLayer.Common.Constants;
using EducationApp.BusinessLogicLayer.Models.User;

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
        public async Task<IActionResult> GetUserByEmail(string userEmail)
        {
            var user = await _userService.GetByEmailAsync(userEmail); 
            return Ok(user);
        }
        
        [HttpPost("edit")]
        [Authorize(Roles = Constants.Roles.AllRoles)]
        public async Task<IActionResult> EditUser(AccountSigUpModel userEditModel)
        {
            var result = await _userService.EditAsync(userEditModel);
            return Ok(result);
        }

        [HttpPost("remove/{userId}")]
        [Authorize(Roles = Constants.Roles.AdmimRole)]
        public async Task<IActionResult>  RemoveUser(string userId)
        {
            var result = await _userService.RemoveAsync(userId);
            return Ok(result);
        }
    }
}
