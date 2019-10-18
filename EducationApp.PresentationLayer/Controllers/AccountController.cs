using EducationApp.BusinessLogicLayer.Models.Authorization;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using EducationApp.BusinessLogicLayer.Common.Constants;
using EducationApp.BusinessLogicLayer.Helpers;

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
        public AccountController(IUserService userService, IAccountService accountService, IOptionsMonitor<AuthTokenProviderOptionsModel> options,JwtHelper jwtHelper)
        {
            _userService = userService;
            _accountService = accountService;
           _options = options;
            _jwtHelper = jwtHelper;
        }


        [HttpPost("sigin")]
        public async Task<IActionResult> SigIn(AccountSigInModel accountSigInModel)
        {
            var result = await _accountService.SigInAsync(accountSigInModel);
            return Ok(result);
        }

        [HttpPost("sigup")]
        public async Task<IActionResult> SigUp(AccountSigUpModel accountSigUpModel)
        {
            var result = await _accountService.SigUpAsync(accountSigUpModel);
            if (!result.Errors.Any())
            {
                return Ok(result);
            }
            var applicationUser = await _userService.GetByEmailAsync(accountSigUpModel.Email);
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
            if (!_jwtHelper.CheckExpiration(jwtTokensModel.RefreshToken))
            {
                return null;
            }
            var readRefreshToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtTokensModel.RefreshToken);
            var userId = readRefreshToken.Claims.Where(x => x.Type.Equals(ClaimTypes.NameIdentifier)).FirstOrDefault().Value;
            var userModelItem = await _userService.GetByIdAsync(userId);
            var userRole = await _accountService.GetRoleAsync(userModelItem.Email);
            var jwtTokens = _jwtHelper.Refresh(jwtTokensModel.RefreshToken, userRole, userModelItem, _options);
            return jwtTokens;
        }
    }
}
