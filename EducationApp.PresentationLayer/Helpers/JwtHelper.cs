using EducationApp.BusinessLogicLayer.Models.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using EducationApp.BusinessLogicLayer.Models.User;

namespace EducationApp.PresentationLayer.Helpers
{
    public class JwtHelper
    {
        private readonly AuthTokenProviderOptionsModel _options;
        public JwtHelper(IOptionsMonitor<AuthTokenProviderOptionsModel> options)
        {
            _options = options.CurrentValue;
        }

        public string GenerateAccessToken(UserModelItem user ,string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = GenerateToken(claims, _options.AccessTokenExpiration);
            return token;
        }
        public string GenerateRefreshToken(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userId)
            };
            var token = GenerateToken(claims, _options.RefreshTokenExpiration);            
            return token;
        }

        public string GenerateToken(List<Claim> claims,TimeSpan expirationTime)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _options.JwtIssuer,
                _options.JwtIssuer,
                claims,
                expires: DateTime.UtcNow + expirationTime,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool CheckExpiration(string token)
        {
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            if (jwtSecurityToken.ValidTo > DateTime.Now)
            {
                return true;
            }
            return false;
        }
        
        public JwtTokensModel Refresh(string refreshToken,string userRole,UserModelItem user)
        {
            var readRefreshToken= new JwtSecurityTokenHandler().ReadJwtToken(refreshToken);
            var userId = readRefreshToken.Claims.Where(x => x.Type.Equals(ClaimTypes.NameIdentifier)).FirstOrDefault().Value;
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }
            if (readRefreshToken.Claims.Where(x => x.Type.Equals(JwtRegisteredClaimNames.Jti)).FirstOrDefault().Value == null)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(userRole))
            {
                return null;
            }
            var newAccessToken = GenerateAccessToken(user,userRole);
            var newRefreshToken = GenerateRefreshToken(userId);
            if (string.IsNullOrWhiteSpace(newAccessToken) || string.IsNullOrWhiteSpace(newRefreshToken))
            {
                return null;
            }
            return new JwtTokensModel()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
