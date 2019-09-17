using EducationApp.BusinessLogicLayer.Models.Enums;
using EducationApp.BusinessLogicLayer.Models.PrintingEdition;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    class OrderModelItem
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public Currency Currency { get; set; }
        public int PrintingEditionsId { get; set; }
        public PrintingEditionModel PrintingEditons { get; set; }
        public int Count { get; set; }
    }
}
