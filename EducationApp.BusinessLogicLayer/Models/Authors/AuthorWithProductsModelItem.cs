using EducationApp.DataAccessLayer.Models.Author;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.Authors
{
    public class AuthorWithProductsModelItem
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public List<string> Products;
        public AuthorWithProductsModelItem(AuthorWithProductsModel items)
        {
            AuthorId = items.AuthorId;
            Name = items.AuthorName;
            Products = items.Title;
        }
    }
}
