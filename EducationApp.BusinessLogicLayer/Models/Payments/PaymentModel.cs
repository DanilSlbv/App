using EducationApp.BusinessLogicLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

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
