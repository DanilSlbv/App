using EducationApp.BusinessLogicLayer.Models.Base;
using System;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class EditOrderModel:BaseModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public int PaymentId { get; set; }
    }
}
