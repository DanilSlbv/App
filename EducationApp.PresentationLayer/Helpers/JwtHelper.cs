using EducationApp.BusinessLogicLayer.Models.Authorization;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Helpers
{
    public class JwtHelper
    {
        private readonly AuthTokenProviderOptions _options;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        public JwtHelper(IOptionsMonitor<AuthTokenProviderOptions> options,IAccountService  accountService,IUserService userService)
        {
            _options = options.CurrentValue;
            _accountService = accountService;
            _userService = userService;
        }

        public string GenerateAccessToken(string userId, string userEmail,string userName,string role )
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userEmail),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _options.JwtIssuer,
                _options.JwtIssuer,
                claims,
                expires: DateTime.UtcNow+_options.AccessTokenExpiration,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateRefreshToken(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _options.JwtIssuer,
                _options.JwtIssuer,
                claims,
                expires: DateTime.UtcNow +_options.RefreshTokenExpiration,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool CheckExpiration(string token)
        {
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            if (jwtSecurityToken.ValidTo > DateTime.Now)
            {
                return false;
            }
            return true;
        }
        public ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JwtKey)),
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }
            return principal;
        }

        public async Task<JwtTokens>  Refresh(string refreshToken)
        {
            ClaimsPrincipal claimsPrincipal = GetClaimsPrincipalFromToken(refreshToken);
            if (!claimsPrincipal.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
            {
                return null;
            }
            if (claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) == null)
            {
                return null;
            }
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.GetByIdAsync(userId);
            var userRole = await _accountService.GetRoleAsync(user.Email);
            if (userRole == null)
            {
                return null;
            }
            var newAccessToken = GenerateAccessToken(userId, user.Email, user.Email, userRole[0]);
            var newRefreshToken = GenerateRefreshToken(userId);
            if (newAccessToken == null || newRefreshToken == null)
            {
                return null;
            }
            return new JwtTokens() { accessToken = newAccessToken, refreshToken = newRefreshToken };
        }
    }
}
