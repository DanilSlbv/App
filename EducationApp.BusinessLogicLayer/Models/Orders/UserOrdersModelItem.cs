using System.Collections.Generic;
using System;
using EducationApp.DataAccessLayer.Models.Order;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class UserOrdersModelItem
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<string> PrintingType { get; set; }
        public List<string> PrintingTitle { get; set; }
        public double OrderAmount { get; set; }
        public UserOrdersModelItem(OrdersForUserModel orders)
        {
            OrderId = orders.OrderId;
            OrderDate = orders.OrderDate;
            PrintingType= orders.PrintingType;
            PrintingTitle = orders.PrintingTitle;
            OrderAmount = orders.OrderAmount;
        }
    }
}
