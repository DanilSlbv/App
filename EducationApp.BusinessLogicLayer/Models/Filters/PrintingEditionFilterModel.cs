using Type=EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;
using Currency= EducationApp.BusinessLogicLayer.Models.Enums.Enums.Currency;
using Price= EducationApp.BusinessLogicLayer.Models.Enums.Enums.AscendingDescending;

namespace EducationApp.BusinessLogicLayer.Models.Filters
{
    public class PrintingEditionFilterModel
    {
        public Type Type { get; set; }
        public Currency Currency { get; set; }
        public Price Price { get; set; }
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }
    }
}
