using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Common.Extensions
{
    public partial class Constants
    {
        public partial class EmailHelper
        {
            public const string EmailFromWho = "BookStore";
            public const string EmailSubject = "BookStore - Confirm the Email";
            public const string EmailConfirmBody = "To Confirm the email please follow the link:";
            public const string PasswordRecoveryEmailBody = "To access Bookstore ,use new password: ";
            public const string Host = "smtp.gmail.com";
            public const string MailAddress = "BookStore@book.com";
            public const string ConfirmEmailLink = "http://localhost:4200/account/emailconfirm/";
            public const string RecoveryPasswordLink= "http://localhost:4200/account/recoverypassword";
        }
        public partial class JwtConstants
        {
            public const string NameIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            public const string Jti = "jti";
        }
    }
}

