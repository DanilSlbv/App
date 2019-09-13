using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.User
{
    public class AccountSigInModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool isPersitent { get; set; }
    }
}
