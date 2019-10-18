using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Models.Orders;
using System;
using PrintingType = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;

namespace EducationApp.BusinessLogicLayer.Mapper
{
    public static class OrderMapper
    {
        public static Order MapToOrder(string description, long paymentId, string userId)
        {
            return new Order
            {
                Date = DateTime.UtcNow,
                Description = description,
                PaymentId = paymentId,
                UserId = userId
            };
        }
        public static OrderModelItem MapToOrderModelItem(Order order)
        {
            return new OrderModelItem
            {
                Description = order.Description,
                UserId = order.UserId,
                Date = order.Date,
                PaymentId = order.PaymentId
            };
        }

        public static OrdersWithOrderItemsModelItem MapToOrdersWithOrderItemsModelItem(OrdersWithOrderItemsModel orders)
        {
            var resultOrders = new OrdersWithOrderItemsModelItem
            {
                OrderId = orders.OrderId,
                OrderDate = orders.OrderDate,
                UserEmail = orders.UserEmail,
                OrderAmount = orders.OrderAmount
            };
            foreach(var printingEdition in orders.PrintingEditions)
            {
                resultOrders.PrintingEditions.Add(PrintingEditionMapper.MapToPrintingEditionModelItem(printingEdition));
            }
            foreach(var printingType in orders.PrintingTypes)
            {
                resultOrders.PrintingTypes.Add((PrintingType)printingType);
            }
            return resultOrders;
        }
    }
}
