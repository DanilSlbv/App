using Type = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;
using Currency = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Currency;
using EducationApp.BusinessLogicLayer.Models.Authors;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.PrintingEdition
{
    public class PrintingEditionsWithAuthor
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<AuthorModelItem> Authors { get; set; }
        public Currency Currency { get; set; }
        public Type Type { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public PrintingEditionsWithAuthor()
        {
            Authors = new List<AuthorModelItem>();
        }
    }
}
