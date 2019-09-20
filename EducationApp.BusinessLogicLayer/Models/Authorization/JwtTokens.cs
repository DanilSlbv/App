using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.Authorization
{
    public class JwtTokens
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
