using EducationApp.BusinessLogicLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.Payments
{
    class PaymentItemModel: BaseModel
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
    }
}
