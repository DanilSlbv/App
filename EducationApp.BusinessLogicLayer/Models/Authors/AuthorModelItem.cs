﻿using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.Authors
{
    public class AuthorModelItem:BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AuthorModelItem(Author author)
        {
            Id = author.Id;
            Name = author.Name;
        }
    }
}
