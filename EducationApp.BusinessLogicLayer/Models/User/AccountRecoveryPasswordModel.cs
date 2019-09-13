using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.User
{
    public class AccountRecoveryPasswordModel
    {
        public string id { get; set; }
        public string recoveryToken { get; set; }
        public string NewPassword { get; set; }
    }
}
