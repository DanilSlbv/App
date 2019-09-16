using EducationApp.BusinessLogicLayer.Helpers;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.PresentationLayer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
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
        private readonly IUrlHelper urlHelper;
        private readonly IApplicationBuilder _applicationBuilder;

        public AccountController(IUserService userService, IAccountService accountService, IConfiguration configuration,IApplicationBuilder applicationBuilder)
        {
            _userService = userService;
            _accountService = accountService;
            _configuration = configuration;
            _applicationBuilder = applicationBuilder;
        }
        
        [HttpPost("sigin")]
        public async Task<IActionResult> SigIn(AccountSigInModel accountSigInModel)
        {
            var result = await _accountService.SigInAsync(accountSigInModel);
            if (result)
            {
                var applicationUser = await _userService.GetByEmailAsync(accountSigInModel.Email);
                JwtHelper jwtHelper = new JwtHelper(_configuration);
                Refresh(applicationUser.Id, accountSigInModel.Email);
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("account")]
        public async Task<IActionResult> SigUp(AccountSigUpModel accountSigInModel)
        {
            try
            {
                var applicationUser = await _accountService.SigUpAsync(accountSigInModel);
                var code = await _accountService.GenerateUserEmailConfrimTokenAsync(applicationUser.Id);
                var callbackUrl = UrlHelper.EmailConfirmUrl(urlHelper, applicationUser.Id, code, HttpContext.Request.Scheme);
                EmailHelpers emailHelpers = new EmailHelpers(applicationUser.Email, callbackUrl);
                await emailHelpers.SendEmailAsync();
                return Ok(applicationUser);
            }catch(Exception ex)
            {
                return Ok(ex.ToString());
            }
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
                var callbackUrl = UrlHelper.PasswordConfirmUrl(urlHelper, applicationUser.Id, recoveryToken, HttpContext.Request.Scheme);
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
        public async Task<IActionResult> ConfirmEmail(string userId,string userEmail,string code)
        {
            if (!await _accountService.ConfirmEmailAsync(userId, code))
            {
                return Ok(false);
            }
            if (!await _accountService.CheckEmailConfirmAsync(userId))
            {
                return Ok(false);
            }
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
            JwtHelper jwtHelper = new JwtHelper(_configuration);
            var accessToken = jwtHelper.GenerateToken(accessClaim);
            var refreshToken = jwtHelper.GenerateToken(refreshClaim);
            return Ok(accessToken + " " + refreshToken);
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(string userId, string userEmail)
        {
            JwtHelper jwtHelper = new JwtHelper(_configuration);
            if (Response.StatusCode != 401)
            {
                return Ok(true); 
            }
            JwtSecurityToken accessToken =new JwtSecurityTokenHandler().
                ReadJwtToken( jwtHelper.GetAccessTokenFromCookie(_applicationBuilder));
            JwtSecurityToken refreshToken = new JwtSecurityTokenHandler().
                ReadJwtToken(jwtHelper.GetRefreshTokenFromCookie(_applicationBuilder));

            if (accessToken.ValidFrom.AddMinutes(10) != DateTime.UtcNow)
            {
                JwtSecurityToken newAccessToken = new JwtSecurityToken(issuer:refreshToken.Issuer,
                        audience: refreshToken.Issuer,
                        claims: refreshToken.Claims,
                        expires:DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: refreshToken.SigningCredentials);
            }
            if (refreshToken.ValidFrom.AddDays(60) != DateTime.UtcNow)
            {
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
                JwtHelper newJwtHelper = new JwtHelper(_configuration);
                var newAccessToken = jwtHelper.GenerateToken(accessClaim);
                var newRefreshToken = jwtHelper.GenerateToken(refreshClaim);
                return Ok(newAccessToken + " " + newRefreshToken);
            }
            return Ok(true);
        }
    }
}
