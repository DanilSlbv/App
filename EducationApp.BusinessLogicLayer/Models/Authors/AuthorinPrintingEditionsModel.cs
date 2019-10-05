using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.Authors
{
    public class AuthorInPrintingEditionsModel
    {
        public List<AuthorInPrintingEditionsModelItem> Items { get; set; }
        public AuthorInPrintingEditionsModel()
        {
            Items = new List<AuthorInPrintingEditionsModelItem>();
        }
    }
}
