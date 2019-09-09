using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using EducationApp.BusinessLogicLayer.Models.Base;

namespace EducationApp.BusinessLogicLayer.Models.User
{
    public class UserModel:BaseModel
    {
        public List<UserModelItem> Items { get; set; }
        public UserModel()
        {
            Items = new List<UserModelItem>(); 
        }
    }
}
