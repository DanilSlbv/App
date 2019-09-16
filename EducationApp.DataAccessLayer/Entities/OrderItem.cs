
using EducationApp.DataAccessLayer.Entities.Base;
using EducationApp.DataAccessLayer.Entities.Enums;

namespace EducationApp.DataAccessLayer.Entities
{
    public class OrderItem:BaseModel
    {
        public double Amount { get; set; }
        public Currency Currency { get; set; }
        public string PrintingEditionId { get; set; }
        public PrintingEdition PrintingEdition { get; set; }
        public int Count { get; set; }
    }
}
