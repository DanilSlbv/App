using SortByPrintingType = EducationApp.DataAccessLayer.Entities.Enums.Enums.Type;
using SortCurrency = EducationApp.DataAccessLayer.Entities.Enums.Enums.Currency;
using SortByPrice = EducationApp.DataAccessLayer.Entities.Enums.Enums.AscendingDescending;

namespace EducationApp.DataAccessLayer.Models.Filters
{
    public class PrintingEditionFilterModel
    {
        public SortByPrintingType SortByPrintingType { get; set; }
        public SortCurrency SortByCurrency { get; set; }
        public SortByPrice SortByPrice { get; set; }
        public float minPrice { get; set; }
        public float maxPrice { get; set; }
        public string SearchName { get; set; }
    }
}
