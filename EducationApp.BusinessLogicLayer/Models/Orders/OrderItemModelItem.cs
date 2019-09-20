using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.BusinessLogicLayer.Models.Enums;
using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.DataAccessLayer.Entities;
using System;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class OrderItemModelItem:BaseModel
    {
        public double Amount { get; set; }
        public Currency Currency { get; set; }
        public string PrintingEditionId { get; set; }
        public PrintingEditionModelItem printingEditionModelItem{ get; set; }
        public int Count { get; set; }
        public OrderItemModelItem(OrderItem order)
        {
            Amount = order.Amount;
            Currency = (Currency)order.Currency;
            PrintingEditionId = order.PrintingEditionId;
            Count = order.Count;
        }
    }
}
