using EducationApp.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace EducationApp.DataAccessLayer.Models.Author    
{
    public class AuthorWithProductsModel
    {
        public long AuthorId { get; set; }
        public string AuthorName { get; set; }
        public List<PrintingEdition> PrintingEditions { get; set; }
    }
}
