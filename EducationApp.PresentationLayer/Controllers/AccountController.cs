using EducationApp.BusinessLogicLayer.Helpers;
using EducationApp.BusinessLogicLayer.Models.Authorization;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.PresentationLayer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
      

        public AccountController(IUserService userService, IAccountService accountService, IConfiguration configuration)
        {
            _userService = userService;
            _accountService = accountService;
            _configuration = configuration;
        }
        

        [HttpPost("sigin")]
        public async Task<IActionResult> SigIn(AccountSigInModel accountSigInModel)
        {
            var result = await _accountService.SigInAsync(accountSigInModel);
            if (result)
            {
                var applicationUser = await _userService.GetByEmailAsync(accountSigInModel.Email);
                Refresh(applicationUser.Id, accountSigInModel.Email);
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

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId,string code)
        {
            TokenClaims tokenClaims = new TokenClaims();
            JwtHelper jwtHelper = new JwtHelper(_configuration);
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
            
            var accessToken = jwtHelper.GenerateAccessToken(tokenClaims.accessToken(userId, userRole[0], applicationUser.Email));
            var refreshToken = jwtHelper.GenerateRefreshToken(tokenClaims.rereshToken(userId));
            RemoveCookie("accessTokenCookie");
            RemoveCookie("refreshTokenCookie");
            AddToCookie("accessTokenCookie", accessToken);
            AddToCookie("refreshTokenCookie", refreshToken);
            return Ok(accessToken + " " + refreshToken);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(string userId, string userEmail)
        {
                TokenClaims tokenClaims = new TokenClaims();
                JwtHelper jwtHelper = new JwtHelper(_configuration);
                var userRole = await _accountService.GetRoleAsync(userEmail);
                var accessToken = ReadAccessTokenCookie();
                var refreshToken = ReadRefreshTokenCookie();
                if (refreshToken == null)
                {
                    RemoveCookie("accessTokenCookie");
                    RemoveCookie("refreshTokenCookie");
                    var newAccessToken = jwtHelper.GenerateAccessToken(tokenClaims.accessToken(userId,userRole[0],userEmail));
                    var newRefreshToken = jwtHelper.GenerateRefreshToken(tokenClaims.rereshToken(userId));
                    AddToCookie("accessTokenCookie", newAccessToken);
                    AddToCookie("refreshTokenCookie", newRefreshToken);
                    return Ok(newAccessToken + " " + newRefreshToken);
                }
                if (accessToken == null)
                {
                    JwtSecurityToken refreshTokenReader = new JwtSecurityTokenHandler().ReadJwtToken(refreshToken);
                    var newAccessToken = jwtHelper.GenerateAccessToken(tokenClaims.accessToken(userId, userRole[0], userEmail));
                    var newToken = newAccessToken.ToString();
                    AddToCookie("accessTokenCookie", newToken);
                }
                return Ok(true);
        }


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


        public string ReadAccessTokenCookie()
        {
            string accessCookieValue = Request.Cookies["accessTokenCookie"];
           
            return accessCookieValue;
        }

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
