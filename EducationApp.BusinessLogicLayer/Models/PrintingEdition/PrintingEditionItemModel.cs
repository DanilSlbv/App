using EducationApp.BusinessLogicLayer.Models.Enums;
namespace EducationApp.BusinessLogicLayer.Models.PrintingEdition
{
    public class PrintingEditionItemModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsRemoved { get; set; }
        public StatusModel status { get; set; }
        public CurrencyModel currency { get; set; }
        public TypeModel type { get; set; }
        public PrintingEditionItemModel(EducationApp.DataAccessLayer.Entities.PrintingEdition printingEdition)
        {
            Id = printingEdition.Id;
            Name = printingEdition.Name;
            Description = printingEdition.Description;
            Price = printingEdition.Price;
            IsRemoved = printingEdition.IsRemoved;
            status = (StatusModel)printingEdition.status;
            currency = (CurrencyModel)printingEdition.currency;
            type = (TypeModel)printingEdition.type;
        }
    }
}
