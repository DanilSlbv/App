using EducationApp.BusinessLogicLayer.Models.PrintingEdition;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    class OrderItemModel
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public enum Currency { }
        public int PrintingEditionsId { get; set; }
        public PrintingEditionModel PrintingEditons { get; set; }
        public int Count { get; set; }
    }
}
