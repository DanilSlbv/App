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
using System.Linq;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Common.Extensions;

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
        private readonly IOptionsMonitor<AuthTokenProviderOptionsModel> _options;


        public AccountController(IUserService userService, IAccountService accountService, IConfiguration configuration, IOptionsMonitor<AuthTokenProviderOptionsModel> options)
        {
            _userService = userService;
            _accountService = accountService;
            _configuration = configuration;
            _options = options;
        }


        [HttpPost("sigin")]
        public async Task<IActionResult> SigIn(AccountSigInModel accountSigInModel)
        {
            var jwtTokensModel = new JwtTokensModel();
            var result = await _accountService.SigInAsync(accountSigInModel);
            if (result)
            {
                var applicationUser = await _userService.GetByEmailAsync(accountSigInModel.Email);
                var jwtTokenModel = new JwtTokensModel();
                JwtHelper jwtHelper = new JwtHelper(_options, _accountService, _userService);
                var userRole = await _accountService.GetRoleAsync(applicationUser.Email);
                jwtTokenModel.AccessToken = jwtHelper.GenerateAccessToken(applicationUser.Id, applicationUser.Email, applicationUser.Email, userRole[0]);
                jwtTokenModel.RefreshToken = jwtHelper.GenerateRefreshToken(applicationUser.Id);
                return Ok(jwtTokenModel);
            }
            return Ok(null);
        }

        [HttpPost("sigup")]
        public async Task<IActionResult> SigUp(AccountSigUpModel accountSigInModel)
        {
            var sigInResult= await _accountService.SigUpAsync(accountSigInModel);
            if (!sigInResult)
            {
                return Ok(null);
            }
            var applicationUser = await _userService.GetByEmailAsync(accountSigInModel.Email);
            var code = await _accountService.GenerateUserEmailConfrimTokenAsync(applicationUser.Id);
            var callbackUrl = Url.Action(
                    nameof(AccountController.ConfirmEmail),
                    "Account",
                    new { userId = applicationUser.Id, code = code },
                    protocol: HttpContext.Request.Scheme
                    );
            EmailHelpers emailHelpers = new EmailHelpers(applicationUser.Email,_configuration);
            var mail = emailHelpers.MailMessageForEmailConfirm(callbackUrl);
            await emailHelpers.SendEmailAsync(mail);
            return Ok(sigInResult);
        }

        [HttpPost("sendnewpassword/{useremail}")]
        public async Task<IActionResult> SendPasswordRecovery(string userEmail)
        {
            var applicationUser = await _userService.GetByEmailAsync(userEmail);
            if (applicationUser == null)
            {
                return Ok(false);
            }
                EmailHelpers emailHelpers = new EmailHelpers(applicationUser.Email, _configuration);
                var mail = emailHelpers.MailMessageForPasswordRecovery(await _accountService.PasswordRecoveryAsync(userEmail));
                await emailHelpers.SendEmailAsync(mail);
                return Ok(true);
        }

        [HttpPost("sigout")]
        public async Task<IActionResult> SigOut()
        {
                await _accountService.SignOutUserAsycn();
                return Ok(true);  
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId,string code)
        {
            var jwtTokenModel = new JwtTokensModel();
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
            var userName = applicationUser.FirstName + " " + applicationUser.LastName;
            return RedirectPermanent(Constants.EmailHelper.ConfirmEmailLink+userName);
        }


        [HttpPost("refreshtoken")]
        public async Task<JwtTokensModel> Refresh(JwtTokensModel jwtTokensModel)
        {
            var newJwtTokenModel = new JwtTokensModel();
            JwtHelper jwtHelper = new JwtHelper(_options, _accountService, _userService);
            if (!jwtHelper.CheckExpiration(jwtTokensModel.RefreshToken))
            {
                var readRefreshToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtTokensModel.RefreshToken);
                var userId = readRefreshToken.Claims.First(claims=>claims.Type==Constants.JwtConstants.NameIdentifier).Value;
                var userModelItem = await _userService.GetByIdAsync(userId);
                var userRole = await _accountService.GetRoleAsync(userModelItem.Email);
                newJwtTokenModel.AccessToken = jwtHelper.GenerateAccessToken(userModelItem.Id, userModelItem.Email, userModelItem.Email, userRole[0]);
                newJwtTokenModel.RefreshToken = jwtHelper.GenerateRefreshToken(userModelItem.Id);
                return newJwtTokenModel;
            }
            if (jwtTokensModel.AccessToken== "" || !jwtHelper.CheckExpiration(jwtTokensModel.AccessToken))
            {
                var tokens = await jwtHelper.Refresh(jwtTokensModel.RefreshToken);
                return tokens;
            }
            return null;
        }
    }
}
