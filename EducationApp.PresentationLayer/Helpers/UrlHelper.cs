//using EducationApp.PresentationLayer.Controllers;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace EducationApp.PresentationLayer.Helpers
//{
//    public static class UrlHelper
//    {
//        public static string EmailConfirmUrl(this IUrlHelper urlHelper, string userId, string recoveryToken,string scheme)
//        {
//            var callbackUrl = urlHelper.Action(
//                                 nameof(AccountController.ConfirmEmail),
//                                 "Account",
//                                 new { userId = userId, code = recoveryToken },
//                                 protocol: scheme
//                                 );
//            return callbackUrl;
//        }
//        public static string PasswordConfirmUrl(this IUrlHelper urlHelper, string userId, string recoveryToken, string scheme)
//        {
//            var callbackUrl = urlHelper.Action(
//                                 nameof(AccountController.SendPasswordRecovery),
//                                 "Account",
//                                 new { userId = userId, code = recoveryToken },
//                                 protocol: scheme
//                                 );
//            return callbackUrl;
//        }
//    }
//}
