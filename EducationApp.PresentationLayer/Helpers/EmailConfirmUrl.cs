using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Helpers
{
    public class EmailConfirmUrl
    {

        private readonly IUrlHelper _urlHelper;
        
        public async Task<string> GetUrl(string acionName, string userId, string recoveryToken,string scheme)
        {
            var callbackUrl = _urlHelper.Action(
                                 acionName,
                                 "Account",
                                 new { userId = userId, code = recoveryToken },
                                 protocol: scheme
                                 );
            return callbackUrl;
        }
    }
}
