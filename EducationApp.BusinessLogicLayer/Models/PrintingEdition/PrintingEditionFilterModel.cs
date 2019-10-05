

using Type=EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;
using Currency= EducationApp.BusinessLogicLayer.Models.Enums.Enums.Currency;
using Price= EducationApp.BusinessLogicLayer.Models.Enums.Enums.Price;

namespace EducationApp.BusinessLogicLayer.Models.PrintingEdition
{
    public class PrintingEditionFilterModel
    {
        public Type Type;
        public Currency Currency;
        public Price Price;
        public float minPrice;
        public float maxPrice;
    }
}
