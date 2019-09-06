using System;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Models.Payments;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    class OrderModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public DateTime Date { get; set; }
        public int PaymentId { get; set; }
        public PaymentModel Payment { get; set; }
    }
}
