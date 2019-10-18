using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Models.Authors;

namespace EducationApp.BusinessLogicLayer.Mapper
{
    public static class AuthorMapper
    {

        public static AuthorModelItem MapToAuthorModelItem(Author author)
        {
            return new AuthorModelItem
            {
                Id = author.Id,
                Name = author.Name
            };
        }
        public static AuthorWithProductsModelItem MapToAuthorWithProductsModelItem(AuthorWithProductsModel authors)
        {
            var resultAuthors=new AuthorWithProductsModelItem
            {
                AuthorId = authors.AuthorId,
                Name = authors.AuthorName
            };
            foreach(var author in authors.PrintingEditions)
            {
                resultAuthors.Products.Add(PrintingEditionMapper.MapToPrintingEditionModelItem(author));
            }
            return resultAuthors;
        }
    }
}
