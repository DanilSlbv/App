using EducationApp.BusinessLogicLayer.Models.Enums;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class OrderModelItem
    {
        public string Description { get; set; }
        public string UserId { get; set; }
        public UserModel userModel { get; set; }
        public DateTime Date { get; set; }
        public string PaymentId { get; set; }
        public OrderModelItem(Order order)
        {
            Description = order.Description;
            UserId = order.UserId;
            Date = order.Date;
            PaymentId = order.PaymentId;
        }
    }
}
