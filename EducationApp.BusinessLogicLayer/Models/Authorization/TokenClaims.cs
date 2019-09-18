using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.Authorization
{
    public class TokenClaims
    {
        public List<Claim> rereshToken(string userId)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,userId)
            };
        }
        public List<Claim> accessToken(string userId,string userRole, string userEmail)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,userId),
                new Claim(ClaimTypes.Role,userRole.ToString()),
                new Claim(ClaimTypes.Name,userEmail)
            };
        }
    }
}
