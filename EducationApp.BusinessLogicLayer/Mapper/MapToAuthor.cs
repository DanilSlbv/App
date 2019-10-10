using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.DataAccessLayer.Models.Author;

namespace EducationApp.BusinessLogicLayer.Mapper
{
    public static class MapToAuthor
    {
        public static AuthorWithProductsModelItem MapToAuthorWithProductsModelItem(AuthorWithProductsModel authors)
        {
            var resultAuthors=new AuthorWithProductsModelItem
            {
                AuthorId = authors.AuthorId,
                Name = authors.AuthorName
            };
            foreach(var author in authors.PrintingEditions)
            {
                resultAuthors.Products.Add(MapToPrintingEditions.MapToPrintingEditionModelItem(author));
            }
            return resultAuthors;
        }
    }
}
