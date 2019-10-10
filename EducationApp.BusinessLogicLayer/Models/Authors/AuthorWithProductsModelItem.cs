using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.DataAccessLayer.Models.Author;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.Authors
{
    public class AuthorWithProductsModelItem
    {
        public long AuthorId { get; set; }
        public string Name { get; set; }
        public List<PrintingEditionModelItem> Products;
        public AuthorWithProductsModelItem()
        {
            Products = new List<PrintingEditionModelItem>();
        }
    }
}
