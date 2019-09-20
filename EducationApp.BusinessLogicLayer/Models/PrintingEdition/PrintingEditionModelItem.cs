using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.BusinessLogicLayer.Models.Enums;
namespace EducationApp.BusinessLogicLayer.Models.PrintingEdition
{
    public class PrintingEditionModelItem: BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsRemoved { get; set; }
        public Status status { get; set; }
        public Currency currency { get; set; }
        public Type type { get; set; }
        public PrintingEditionModelItem(EducationApp.DataAccessLayer.Entities.PrintingEdition printingEdition)
        {
            Name = printingEdition.Name;
            Description = printingEdition.Description;
            Price = printingEdition.Price;
            IsRemoved = printingEdition.IsRemoved;
            status = (Status)printingEdition.Status;
            currency = (Currency)printingEdition.Currency;
            type = (Type)printingEdition.Type;
        }
    }
}
