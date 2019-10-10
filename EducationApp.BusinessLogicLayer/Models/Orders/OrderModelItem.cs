using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.DataAccessLayer.Entities;
using System;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class OrderModelItem
    {
        public string Description { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public long PaymentId { get; set; }
        public OrderModelItem()
        {

        }
    }
}
