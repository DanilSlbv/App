using EducationApp.DataAccessLayer.Entities.Base;
using System;

namespace EducationApp.DataAccessLayer.Entities
{
    public class Order:BaseModel
    {
        public string Description { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public DateTime Date { get; set; }
        public string PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
