using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Common.Constants
{
    public partial class Constants
    {
        public partial class Errors
        {
            public const string NotFount = "User not found";
            public const string SigInWrongForm = "Wrong email or password";
            public const string ErrorToUpdate = "Update database-Error";
            public const string SigInError = "Sign in error";
            public const string ConfirmEmailError = "Please confirm email";
            public const string IdIsNull = "Id can't be null";
            public const string UserEmailIsNull = "Email can not be null";
            public const string TokenError = "Unable to get the token";
        }
    }
}
