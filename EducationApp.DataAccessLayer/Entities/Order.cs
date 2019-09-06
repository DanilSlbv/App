using EducationApp.DataAccessLayer.Entities.Base;

namespace EducationApp.DataAccessLayer.Entities
{
    public class Order:BaseModel
    {
        public string Description { get; set; }
        public int UserId { get; set; }
        public int PaymentId { get; set; }
    }
}
