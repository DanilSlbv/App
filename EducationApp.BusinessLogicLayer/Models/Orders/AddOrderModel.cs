using EducationApp.BusinessLogicLayer.Models.Base;
using System;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class AddOrderModel:BaseModel
    {
        public string Description { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string PaymentId { get; set; }
    }
}
