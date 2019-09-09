using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.PresentationLayer.Controllers.Base;
namespace EducationApp.PresentationLayer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController:Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult>GetUser()
        {
            var User =await  _userService.GetUserByIdAsync("1");
            return Ok(User);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserSigUpModel userSigInModel)
        {
            await _userService.SigUpUserAsync(userSigInModel);
            return Content("SignUp");
        }
       
    }
}
