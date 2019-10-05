using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.Authorization
{
    public class JwtTokensModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
