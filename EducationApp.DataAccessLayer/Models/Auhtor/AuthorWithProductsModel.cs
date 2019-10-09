using System.Collections.Generic;

namespace EducationApp.DataAccessLayer.Models.Author    
{
    public class AuthorWithProductsModel
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public List<string> Title { get; set; }
    }
}
