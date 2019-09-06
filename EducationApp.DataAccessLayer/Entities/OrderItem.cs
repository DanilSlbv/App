
using EducationApp.DataAccessLayer.Entities.Base;
namespace EducationApp.DataAccessLayer.Entities
{
    public class OrderItem:BaseModel
    {
       
        public double Amount { get; set; }
        public enum Currency { }
        public int PrintingEditionsId { get; set; }
        public int Count { get; set; }
    }
}
