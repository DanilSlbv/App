using EducationApp.BusinessLogicLayer.Helpers;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.PresentationLayer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
            if (!await _accountService.ConfirmEmailAsync(userId, code))
            {
                return Ok(false);
            }
            if (!await _accountService.CheckEmailConfirmAsync(userId))
            {
                return Ok(false);
            }
            UserModelItem userModelItem =await _userService.GetByIdAsync(userId);
            var accessClaim = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,userModelItem.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId,userModelItem.Id),
                new Claim(ClaimTypes.Role,"user")
            };
            var refreshClaim = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId,userId)
            };
            JwtHelper jwtHelper = new JwtHelper(_configuration);
            var accessToken = jwtHelper.GenerateToken(accessClaim);
            var refreshToken = jwtHelper.GenerateToken(refreshClaim);
            AddToCookie("accessTokenCookie", accessToken);
            AddToCookie("refreshTokenCookie", refreshToken);
            return Ok(accessToken + " " + refreshToken);
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(string userId, string userEmail)
        {
            try
            {
                JwtHelper jwtHelper = new JwtHelper(_configuration);
                var accessToken = ReadAccessTokenCookie();
                var refreshToken = ReadRefreshTokenCookie();
                if (refreshToken == null)
                {
                    RemoveCookie("accessTokenCookie");
                    RemoveCookie("refreshTokenCookie");
                    var accessClaim = new List<Claim>
                    {
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier,userId),
                    new Claim(ClaimTypes.Role,"user"),
                    new Claim(ClaimTypes.Name,userEmail)
                    };
                    var refreshClaim = new List<Claim>
                    {
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier,userId)
                    };
                    var newAccessToken = jwtHelper.GenerateToken(accessClaim);
                    var newRefreshToken = jwtHelper.GenerateToken(refreshClaim);
                    AddToCookie("accessTokenCookie", newAccessToken);
                    AddToCookie("refreshTokenCookie", newRefreshToken);
                    return Ok(newAccessToken + " " + newRefreshToken);
                }
                if (accessToken == null)
                {
                    JwtSecurityToken refreshTokenReader = new JwtSecurityTokenHandler().ReadJwtToken(refreshToken);
                    JwtSecurityToken newAccessToken = new JwtSecurityToken(issuer: refreshTokenReader.Issuer,
                            audience: refreshTokenReader.Issuer,
                            claims: refreshTokenReader.Claims,
                            expires: DateTime.UtcNow.AddMinutes(10),
                            signingCredentials: refreshTokenReader.SigningCredentials);
                    var newToken = newAccessToken.ToString();
                    AddToCookie("accessTokenCookie", newToken);
                }
                return Ok(true);
            }
            catch(Exception ex)
            {
                return  Ok(ex);
            }
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
        public void RemoveCookie(string cookieName)
        {
            Response.Cookies.Delete(cookieName);
        }
    }
}
