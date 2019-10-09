using Status = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Status;
using Type = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;
using Currency = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Currency;

namespace EducationApp.BusinessLogicLayer.Models.PrintingEdition
{
    public class PrintingEditionModelItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsRemoved { get; set; }
        public Status Status { get; set; }
        public Currency Currency { get; set; }
        public Type Type { get; set; }
        public PrintingEditionModelItem() { }
        public PrintingEditionModelItem(EducationApp.DataAccessLayer.Entities.PrintingEdition printingEdition)
        {
            Id = printingEdition.Id;
            Name = printingEdition.Name;
            Description = printingEdition.Description;
            Price = printingEdition.Price;
            IsRemoved = printingEdition.IsRemoved;
            Status = (Status)printingEdition.Status;
            Currency = (Currency)printingEdition.Currency;
            Type = (Type)printingEdition.Type;
        }
    }
}
