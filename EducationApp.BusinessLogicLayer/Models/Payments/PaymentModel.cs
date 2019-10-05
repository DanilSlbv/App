using EducationApp.BusinessLogicLayer.Models.Base;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.Payments
{
    public class PaymentModel:BaseModel
    {
        public List<PaymentItemModel> Items { get; set; }
        public PaymentModel()
        {
            Items = new List<PaymentItemModel>();
        }
    }
}
