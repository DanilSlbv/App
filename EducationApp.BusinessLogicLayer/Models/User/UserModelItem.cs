﻿using System;
using System.Collections.Generic;
using System.Text;
using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.DataAccessLayer.Entities;

namespace EducationApp.BusinessLogicLayer.Models.User
{

    public class UserModelItem
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserModelItem(ApplicationUser User)
        {
            FirstName = User.FirstName;
            LastName = User.LastName;
            Email = User.Email;
        }
    }
    
}