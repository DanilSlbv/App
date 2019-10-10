using Type = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;
using Currency = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Currency;

namespace EducationApp.BusinessLogicLayer.Models.PrintingEdition
{
    public class PrintingEditionModelItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Currency Currency { get; set; }
        public Type Type { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public PrintingEditionModelItem()
        {

        }
    }
}
