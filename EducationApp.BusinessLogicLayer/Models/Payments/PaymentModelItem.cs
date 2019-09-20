using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.Payments
{
    public class PaymentItemModel: BaseModel
    {
        public string Id { get; set; }
        public string TransactionId { get; set; }
        public PaymentItemModel(Payment payment)
        {
            Id = payment.Id;
            TransactionId = payment.TransactionId;
        }
    }
}
