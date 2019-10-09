using Type = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;
using Currency = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Currency;
using EducationApp.DataAccessLayer.Entities;

namespace EducationApp.BusinessLogicLayer.Models.PrintingEdition
{
    public class PrintingEditionsWithAuthor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public Currency Currency { get; set; }
        public Type Type { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public PrintingEditionsWithAuthor(AuthorInPrintingEditons Items)
        {
            Id = Items.PrintingEdition.Id;
            Name = Items.PrintingEdition.Name;
            AuthorName = Items.Author.Name;
            Currency = (Currency)Items.PrintingEdition.Currency;
            Type = (Type)Items.PrintingEdition.Type;
            Price = Items.PrintingEdition.Price;
            Description = Items.PrintingEdition.Description;
        }
    }
}
