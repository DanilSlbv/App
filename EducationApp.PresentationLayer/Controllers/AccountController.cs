using EducationApp.BusinessLogicLayer.Helpers;
using EducationApp.BusinessLogicLayer.Models.Authorization;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.PresentationLayer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
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
        private readonly IOptionsMonitor<AuthTokenProviderOptions> _options;


        public AccountController(IUserService userService, IAccountService accountService, IConfiguration configuration, IOptionsMonitor<AuthTokenProviderOptions> options)
        {
            _userService = userService;
            _accountService = accountService;
            _configuration = configuration;
            _options = options;
        }
        

        [HttpPost("sigin")]
        public async Task<IActionResult> SigIn(AccountSigInModel accountSigInModel)
        {
            var result = await _accountService.SigInAsync(accountSigInModel);
            if (result)
            {
                var applicationUser = await _userService.GetByEmailAsync(accountSigInModel.Email);
                await Refresh(applicationUser);
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("sigup")]
        public async Task<IActionResult> SigUp(AccountSigUpModel accountSigInModel)
        {
                var applicationUser = await _accountService.SigUpAsync(accountSigInModel);
                var code = await _accountService.GenerateUserEmailConfrimTokenAsync(applicationUser.Id);
                var callbackUrl = Url.Action(
                                 nameof(AccountController.ConfirmEmail),
                                 "Account",
                                 new { userId = applicationUser.Id, code = code },
                                 protocol: HttpContext.Request.Scheme
                                 );
                EmailHelpers emailHelpers = new EmailHelpers(applicationUser.Email, callbackUrl);
                await emailHelpers.SendEmailAsync();
                return Ok(applicationUser);
        }

        [HttpPost("sendpasswordrecovery")]
        public async Task<IActionResult> SendPasswordRecovery(AccountRecoveryPasswordModel accountRecoveryPasswordModel)
        {
                var applicationUser = await _userService.GetByIdAsync(accountRecoveryPasswordModel.id);
                if (applicationUser == null)
                {
                    return Content("User not Found");
                }
                var recoveryToken = await _accountService.GeneratePasswordResetTokenAsync(accountRecoveryPasswordModel.id);
                if (recoveryToken != null)
                {
                var callbackUrl = Url.Action(
                                 nameof(AccountController.RecoveryPassword),
                                 "Account",
                                 new { userId = applicationUser.Id, code = recoveryToken },
                                 protocol: HttpContext.Request.Scheme
                                 );
                EmailHelpers emailHelpers = new EmailHelpers(accountRecoveryPasswordModel.id, callbackUrl);
                    await emailHelpers.SendEmailAsync();
                    return Content("Email Send");
                }
               
                return Content("Token is Null");
        }

        [HttpPost("recoverypassword")]
        public async Task<IActionResult> RecoveryPassword(AccountRecoveryPasswordModel accountRecoveryPasswordModel)
        {
            var result = await _accountService.PasswordRecoveryAsync(accountRecoveryPasswordModel.id, accountRecoveryPasswordModel.recoveryToken, accountRecoveryPasswordModel.NewPassword);
            if (result)
            {
                return Content("PasswordChanged");
            }
            return Content("PasswordChange--Error");
        }
        [HttpPost("sigout")]
        public async Task<IActionResult> SigOut()
        {
                await _accountService.SignOutUserAsycn();
                return Ok(true);  
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId,string code)
        {
            var applicationUser = await _userService.GetByIdAsync(userId);
            var userRole = await _accountService.GetRoleAsync(applicationUser.Email);
            if (!await _accountService.ConfirmEmailAsync(userId, code))
            {
                return Ok(false);
            }
            if (!await _accountService.CheckEmailConfirmAsync(userId))
            {
                return Ok(false);
            }
            JwtHelper jwtHelper = new JwtHelper(_options,_accountService,_userService);
            var accessToken = jwtHelper.GenerateAccessToken(userId,applicationUser.Email,applicationUser.Email,userRole[0]);
            var refreshToken = jwtHelper.GenerateRefreshToken(userId);
            AddToCookie("accessTokenCookie", accessToken);
            AddToCookie("refreshTokenCookie", refreshToken);
            return Ok(accessToken + " " + refreshToken);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(UserModelItem userModelItem)
        {
            JwtHelper jwtHelper = new JwtHelper(_options, _accountService, _userService);
            var accessTokenCookie = ReadAccessTokenCookie();
            var refreshTokenCookie = ReadRefreshTokenCookie();
            if (refreshTokenCookie == null || !jwtHelper.CheckExpiration(refreshTokenCookie))
            {
                var userRole = await _accountService.GetRoleAsync(userModelItem.Email);
                var accessToken = jwtHelper.GenerateAccessToken(userModelItem.Id, userModelItem.Email, userModelItem.Email, userRole[0]);
                var refreshToken = jwtHelper.GenerateRefreshToken(userModelItem.Id);
                AddToCookie("accessTokenCookie", accessToken);
                AddToCookie("refreshTokenCookie", refreshToken);
                return Ok(accessToken + " " + refreshToken);
            }
            if (accessTokenCookie==null||!jwtHelper.CheckExpiration(accessTokenCookie))
            {
                var tokens=await jwtHelper.Refresh(refreshTokenCookie);
                AddToCookie("accessTokenCookie", tokens.accessToken);
                AddToCookie("refreshTokenCookie", tokens.refreshToken);
                return Ok(tokens.accessToken + "" + tokens.refreshToken);
            }
            
            return Ok(false);
        }

        [NonAction]
        public void AddToCookie(string name,string value)
        {
            CookieOptions options = new CookieOptions();
            if (name == "accessTokenCookie")
            {
                options.Expires = DateTime.UtcNow.AddMinutes(10);
            }
            if (name == "refreshTokenCookie")
            {
                options.Expires = DateTime.UtcNow.AddDays(60);
            }
            Response.Cookies.Append(name, value, options);
        }

        [NonAction]
        public string ReadAccessTokenCookie()
        {
            string accessCookieValue = Request.Cookies["accessTokenCookie"];
           
            return accessCookieValue;
        }

        [NonAction]
        public string ReadRefreshTokenCookie()
        {
            string refreshCookieValue = Request.Cookies["refreshTokenCookie"];
            return refreshCookieValue;
        }
        [HttpGet("remove/{cookieName}")]
        public void RemoveCookie(string cookieName)
        {
            Response.Cookies.Delete(cookieName);
        }
    }
}
