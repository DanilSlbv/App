using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public bool AddToCookie(IApplicationBuilder applicationBuilder,string accessToken,string refreshToken)
        {
            applicationBuilder.Run(async (context) =>
            {
                context.Response.Cookies.Append("accessTokenCookie", accessToken);
                context.Response.Cookies.Append("refreshTokenCookie", refreshToken);
            });
            return true;
        }
        public string GetAccessTokenFromCookie(IApplicationBuilder applicationBuilder)
        {
            string accessToken="";
            applicationBuilder.Run(async (context) =>
            {
                if (context.Request.Cookies.ContainsKey("accessTokenCookie"))
                {
                    accessToken = context.Request.Cookies["accessTokenCookie"];
                }
            });
            return accessToken;
        }
        public string GetRefreshTokenFromCookie(IApplicationBuilder applicationBuilder)
        {
            string refreshToken = "";
            applicationBuilder.Run(async (context) =>
            {
                if (context.Request.Cookies.ContainsKey("refreshTokenCookie"))
                {
                    refreshToken = context.Request.Cookies["refreshTokenCookie"];
                }
            });
            return refreshToken;
        }
    }
}
