using EducationApp.BusinessLogicLayer.Models.Base;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.Authors
{
    public class AuthorModel:BaseModel
    {
        public List<AuthorModelItem> Items { get; set; }
        public AuthorModel()
        {
            Items = new List<AuthorModelItem>();
        }
    }
}
