using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.DataAccessLayer.Entities;
using Currency = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Currency;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class OrderItemModelItem:BaseModel
    {
        public double Amount { get; set; }
        public Currency Currency { get; set; }
        public int PrintingEditionId { get; set; }
        public int Count { get; set; }
        public int OrderId { get; set; }
        public OrderItemModelItem(OrderItem order)
        {
            Amount = order.Amount;
            Currency = (Currency)order.Currency;
            PrintingEditionId = order.PrintingEditionId;
            Count = order.Count;
        }
    }
}
