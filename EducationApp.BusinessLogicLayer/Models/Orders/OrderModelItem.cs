using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.DataAccessLayer.Entities;
using System;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class OrderModelItem
    {
        public string Description { get; set; }
        public string UserId { get; set; }
        public string userId{ get; set; }
        public DateTime Date { get; set; }
        public int PaymentId { get; set; }
        public OrderModelItem() { }
        public OrderModelItem(Order order)
        {
            Description = order.Description;
            UserId = order.UserId;
            Date = order.Date;
            PaymentId = order.PaymentId;
        }
    }
}
