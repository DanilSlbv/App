using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EducationApp.PresentationLayer.Controllers;

namespace EducationApp.PresentationLayer.Helpers
{
    public class EmailHelper:Controller
    {
        IUrlHelper urlHelper;
        public string GetUrl (string userId, string code)
        {

            var callbackUrl = urlHelper.Action(
                "SigUp",
                "Account",
                new {userId=userId,code=code},
                protocol:HttpContext.Request.Scheme
                );
            return callbackUrl;
            
        }
    }
}
