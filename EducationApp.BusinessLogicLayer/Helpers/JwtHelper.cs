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

namespace EducationApp.BusinessLogicLayer.Helpers
{
    public class JwtHelper
    {
       
        public string GenerateAccessToken(UserModelItem user ,string role,IOptionsMonitor<AuthTokenProviderOptionsModel> options)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = GenerateToken(claims, options.CurrentValue.AccessTokenExpiration, options);
            return token;
        }
        public string GenerateRefreshToken(string userId, IOptionsMonitor<AuthTokenProviderOptionsModel> options)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userId)
            };
            var token = GenerateToken(claims, options.CurrentValue.RefreshTokenExpiration, options);            
            return token;
        }

        public string GenerateToken(List<Claim> claims,TimeSpan expirationTime, IOptionsMonitor<AuthTokenProviderOptionsModel> options)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.CurrentValue.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                options.CurrentValue.JwtIssuer,
                options.CurrentValue.JwtIssuer,
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
        
        public JwtTokensModel Refresh(string refreshToken,string userRole,UserModelItem user, IOptionsMonitor<AuthTokenProviderOptionsModel> options)
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
            var newAccessToken = GenerateAccessToken(user,userRole,options);
            var newRefreshToken = GenerateRefreshToken(userId,options);
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
