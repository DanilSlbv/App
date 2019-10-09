using EducationApp.DataAccessLayer.Models.Order;
using System;
using System.Collections.Generic;
using PrintingType = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class AdminOrdersModelItem
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserEmail { get; set; }
        public List<string> PrintingType { get; set; }
        public List<string> PrintingTitle { get; set; } 
        public double OrderAmount { get; set; }
        public AdminOrdersModelItem(OrdersForAdminModel order)
        {
            OrderId = order.OrderId;
            OrderDate = order.OrderDate;
            UserEmail = order.UserEmail;
            PrintingType = order.PrintingType;
            PrintingTitle = order.PrintingTitle;
            OrderAmount = order.OrderAmount;
        }
    }
}
