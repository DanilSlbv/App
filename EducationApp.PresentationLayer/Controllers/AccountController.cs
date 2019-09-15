using EducationApp.BusinessLogicLayer.Helpers;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Initialization;
using EducationApp.PresentationLayer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Controllers
{
    [Route("account")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;


        public AccountController(IUserService userService,IAccountService accountService,IConfiguration configuration)
        {
            _userService = userService;
            _accountService = accountService;
            _configuration = configuration;
        }
        
        [HttpPost]
        [Route("sigin")]
        public async Task<IActionResult> SigIn(AccountSigInModel accountSigInModel)
        {
            var result = await _accountService.SigInUserAsync(accountSigInModel);
            if (result)
            {
                JwtHelper jwtHelper = new JwtHelper();
                var applicationUser = await _userService.GetUserByEmailAsync(accountSigInModel.Email);
                object resultToken=await jwtHelper.GenerateAccessToken(accountSigInModel.Email, applicationUser, _configuration);
                if (resultToken != null)
                {
                    return Ok(resultToken);
                }
                return Content("SigIn");
            }
            return Content("Wrong");
        }

        [HttpPost]
        [Route("sigup")]
        public async Task<IActionResult> SigUp(AccountSigUpModel accountSigInModel)
        {
                var applicationUser = await _accountService.SigUpUserAsync(accountSigInModel);
                var code = await _accountService.GenerateUserEmailConfrimTokenAsync(applicationUser.Id);
                var callbackUrl =await new EmailConfirmUrl().GetUrl(
                    "ConfirmEmail", 
                    applicationUser.Id, 
                    code, 
                    HttpContext.Request.Scheme);
                EmailHelpers emailHelpers = new EmailHelpers(applicationUser.Email, callbackUrl);
                await emailHelpers.SendEmailAsync();
                return Ok(applicationUser);
        }

        [HttpPost]
        [Route("sendpasswordrecovery")]
        public async Task<IActionResult> SendPasswordRecovery(AccountRecoveryPasswordModel accountRecoveryPasswordModel)
        {
                var applicationUser = await _userService.GetUserByIdAsync(accountRecoveryPasswordModel.id);
                if (applicationUser == null)
                {
                    return Content("User not Found");
                }
                var recoveryToken = await _accountService.GeneratePasswordResetTokenAsync(accountRecoveryPasswordModel.id);
                if (recoveryToken != null)
                {
                    var callbackUrl = await new EmailConfirmUrl().GetUrl(
                        "RecoveryPassword", 
                        applicationUser.Id, 
                        recoveryToken, 
                        HttpContext.Request.Scheme);
                    EmailHelpers emailHelpers = new EmailHelpers(accountRecoveryPasswordModel.id, callbackUrl);
                    await emailHelpers.SendEmailAsync();
                    return Content("Email Send");
                }
               
                return Content("Token is Null");
        }

        [HttpGet]
        [Route("recoverypassword")]
        public async Task<IActionResult> RecoveryPassword(string userId,string code)
        {
            return Ok(new AccountRecoveryPasswordModel() {id =userId,recoveryToken=code});
        }

        [HttpPost]
        [Route("recoverypassword")]
        public async Task<IActionResult> RecoveryPassword(AccountRecoveryPasswordModel accountRecoveryPasswordModel)
        {
            var result = await _accountService.PasswordRecoveryAsync(accountRecoveryPasswordModel.id, accountRecoveryPasswordModel.recoveryToken, accountRecoveryPasswordModel.NewPassword);
            if (result)
            {
                return Content("PasswordChanged");
            }
            return Content("PasswordChange--Error");
        }
        [HttpPost]
        [Route("sigout")]
        public async Task<IActionResult> SigOut()
        {
                await _accountService.SignOutUserAsycn();
                return Ok("SigOut");  
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId,string code)
        {
            var resultConfirm=await _accountService.ConfirmEmailAsync(userId, code);
            if (!resultConfirm)
            {
                return Ok(resultConfirm);
            }
            var resultCheck = await _accountService.CheckEmailConfirmAsync(userId);
            if (!resultCheck)
            {
                return Ok("!resultCheck");
            }
            var applicationUser = await _userService.GetUserByIdAsync(userId);
            JwtHelper jwtHelper = new JwtHelper();
            await jwtHelper.GenerateAccessToken(applicationUser.Email, applicationUser, _configuration);
            if(await _accountService.CanSigInAsync(userId))
            {
                await _accountService.ConfirmEmailAuthorizationAsync(userId);
                return Ok("ConfirmAuthorization");
            }
            return Ok(resultCheck);
        }

        //[HttpPost]
        //[Route("refresh")]
        //public async Task<IActionResult> Refresh(string token, string refreshTooken, string userEmail,)
        //{
        //    RefreshToken refreshToken = new RefreshToken();
        //    var principal = refreshToken.GetPrincipalFromExpiredToken(token);
        //    JwtHelper jwtHelper = new JwtHelper();
        //    var newJwtToken = jwtHelper.GenerateAccessToken(userEmail,)
        //}

        //public async Task<string> GetUrl(string acionName, string userId, string recoveryToken)
        //{
        //    var callbackUrl = Url.Action(
        //                         acionName,
        //                         "Account",
        //                         new { userId = userId, code = recoveryToken },
        //                         protocol: HttpContext.Request.Scheme
        //                         );
        //    return callbackUrl;
        //}
    }
}
