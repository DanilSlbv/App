using EducationApp.BusinessLogicLayer.Models.Authorization;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.PresentationLayer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using EducationApp.BusinessLogicLayer.Common.Constants;

namespace EducationApp.PresentationLayer.Controllers
{
    [Route("account")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IOptionsMonitor<AuthTokenProviderOptionsModel> _options;
        private readonly JwtHelper _jwtHelper;

        public AccountController(IUserService userService, IAccountService accountService,JwtHelper jwtHelper, IOptionsMonitor<AuthTokenProviderOptionsModel> options)
        {
            _userService = userService;
            _accountService = accountService;
            _jwtHelper = jwtHelper;
            _options = options;
        }


        [HttpPost("sigin")]
        public async Task<IActionResult> SigIn(AccountSigInModel accountSigInModel)
        {
            var result = await _accountService.SigInAsync(accountSigInModel);
            if (!result.Errors.Count().Equals(0))
            {
                return Ok(result);
            }
            var applicationUser = await _userService.GetByEmailAsync(accountSigInModel.Email);
            var userRole = await _accountService.GetRoleAsync(applicationUser.Email);
            var tokens = _jwtHelper.Refresh(null, userRole, applicationUser);
            return Ok(tokens);
        }

        [HttpPost("sigup")]
        public async Task<IActionResult> SigUp(AccountSigUpModel accountSigInModel)
        {
            var result = await _accountService.SigUpAsync(accountSigInModel);
            if (!result.Errors.Count().Equals(0))
            {
                return Ok(result);
            }
            var applicationUser = await _userService.GetByEmailAsync(accountSigInModel.Email);
            var code = await _accountService.GenerateUserEmailConfrimTokenAsync(applicationUser.Id);
            var callbackUrl = Url.Action(
                    nameof(AccountController.ConfirmEmail),
                    "Account",
                    new { userId = applicationUser.Id, code = code },
                    protocol: HttpContext.Request.Scheme
                    );
            var sendConfirmEmailResult = await _accountService.SendConfirmEmailAsync(applicationUser.Email, callbackUrl);
            return Ok(sendConfirmEmailResult);
        }

        [HttpPost("sendpasswordrecovery/{useremail}")]
        public async Task<IActionResult> SendPasswordRecovery(string userEmail)
        {
            var result = await _accountService.SendEmailForPasswordRecoveryAsync(userEmail);
            return Ok(result);
        }

        [HttpPost("sigout")]
        public async Task<IActionResult> SigOut()
        {
            await _accountService.SignOutUserAsycn();
            return Ok(true);
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
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
            var userName = applicationUser.FirstName + " " + applicationUser.LastName;
            return RedirectPermanent(Constants.EmailHelper.ConfirmEmailLink + userName);
        }


        [HttpPost("refreshtoken")]
        public async Task<JwtTokensModel> RefreshToken(JwtTokensModel jwtTokensModel)
        {
            var newJwtTokenModel = new JwtTokensModel();
            JwtHelper jwtHelper = new JwtHelper(_options);
            if (!jwtHelper.CheckExpiration(jwtTokensModel.RefreshToken))
            {
                var readRefreshToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtTokensModel.RefreshToken);
                var userId = readRefreshToken.Claims.Where(x => x.Type.Equals(ClaimTypes.NameIdentifier)).FirstOrDefault().Value;
                var userModelItem = await _userService.GetByIdAsync(userId);
                var userRole = await _accountService.GetRoleAsync(userModelItem.Email);
                newJwtTokenModel.AccessToken = jwtHelper.GenerateAccessToken(userModelItem,userRole);
                newJwtTokenModel.RefreshToken = jwtHelper.GenerateRefreshToken(userModelItem.Id);
                return newJwtTokenModel;
            }
            if (string.IsNullOrWhiteSpace(jwtTokensModel.AccessToken)|| !jwtHelper.CheckExpiration(jwtTokensModel.AccessToken))
            {
                var readRefreshToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtTokensModel.RefreshToken);
                var userId = readRefreshToken.Claims.Where(x => x.Type.Equals(ClaimTypes.NameIdentifier)).FirstOrDefault().Value;
                var userModelItem = await _userService.GetByIdAsync(userId);
                var userRole = await _accountService.GetRoleAsync(userModelItem.Email);
                var tokens = jwtHelper.Refresh(jwtTokensModel.RefreshToken,userRole,userModelItem);
                return tokens;
            }
            return null;
        }
    }
}
