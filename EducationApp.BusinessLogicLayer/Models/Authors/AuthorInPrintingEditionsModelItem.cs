using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.Authors
{
    public class AuthorInPrintingEditionsModelItem
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public List<string> Products;
        public AuthorInPrintingEditionsModelItem()
        {
            Products = new List<string>();
        }
    }
}
