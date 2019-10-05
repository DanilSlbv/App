using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.DataAccessLayer.Entities;

namespace EducationApp.BusinessLogicLayer.Models.Payments
{
    public class PaymentItemModel: BaseModel
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public PaymentItemModel(Payment payment)
        {
            Id = payment.Id;
            TransactionId = payment.TransactionId;
        }
    }
}
