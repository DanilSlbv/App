using EducationApp.BusinessLogicLayer.Helpers;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.PresentationLayer.Controllers.Base;
using EducationApp.PresentationLayer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Controllers
{
    [Route("Account")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("sig")]
        public async Task<IActionResult> Sig()
        {
            return Content("Sig");
        }
        [HttpPost]
        [Route("SigIn")]
        public async Task<IActionResult> SigIn(UserSigInModel userSigInModel)
        {
            var result = await _userService.SigInUserAsync(userSigInModel);
            if (result)
            {
                return Content("SigIn");
            }
            return Content("Wrong");
        }
        [HttpPost]
        [Route("SigUp")]
        public async Task<IActionResult> SigUp(UserSigUpModel userSigUpModel)
        {
            EmailHelper emailHelper = new EmailHelper();
            var applicationUser = await _userService.SigUpUserAsync(userSigUpModel);
            var code = await _userService.GenerateUserEmailConfrimAsync(applicationUser.Id);
            var link=emailHelper.GetUrl(applicationUser.Id, code);
            EmailHelpers emailHelpers = new EmailHelpers(applicationUser.Email, link);
            await emailHelpers.SendEmailAsync();
            return Ok(applicationUser);
           
        }
        [HttpPost]
        [Route("SigOut")]
        public async Task<IActionResult> SigOut()
        {
            
                await _userService.SignOutUserAsycn();
                return Ok("SigOut");
           
        }
    }
}
