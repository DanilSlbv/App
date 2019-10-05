using Status = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Status;
using Currency = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Currency;
using Type = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;

namespace EducationApp.BusinessLogicLayer.Models.PrintingEdition
{
    public class EditPrintingEditionModelItem
    {
        public  int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsRemoved { get; set; }
        public Status Status { get; set; }
        public Currency Currency { get; set; }
        public Type Type { get; set; }
    }
}
