using CurrencyConvert = EducationApp.DataAccessLayer.Entities.Enums.Enums.Currency;
using AscDescConvert = EducationApp.DataAccessLayer.Entities.Enums.Enums.AscendingDescending;
using Currency = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Currency;
using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Models.Filters;
using EducationApp.BusinessLogicLayer.Models.Filters;

namespace EducationApp.BusinessLogicLayer.Mapper
{
    public static class OrderItemMapper
    {
        public static OrderItem MapToOrderItem(OrderItemModelItem orderItem)
        {
            return new OrderItem
            {
                Amount = orderItem.Amount,
                Currency = (CurrencyConvert)orderItem.Currency,
                PrintingEditionId = orderItem.PrintingEditionId,
                Count = orderItem.Count,
                OrderId = orderItem.OrderId
            };
        }

        public static OrderFilterModel MapToOrderFilter(OrderFilterModelItem orderFilter)
        {
            return new OrderFilterModel
            {
                SortByOrderId = (AscDescConvert)orderFilter.SortByOrderId,
                SortByDate = (AscDescConvert)orderFilter.SortByDate,
                SortByOrderAmount = (AscDescConvert)orderFilter.SortByOrderAmount
            };
        }

        public static OrderItemModelItem MapToOrderItemModelItem(OrderItem orderItem)
        {
            return new OrderItemModelItem
            {
                Amount = orderItem.Amount,
                Currency = (Currency)orderItem.Currency,
                PrintingEditionId = orderItem.PrintingEditionId,
                Count = orderItem.Count
            };
        }
    }
}
