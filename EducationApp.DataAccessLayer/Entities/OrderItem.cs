using Currency = EducationApp.DataAccessLayer.Entities.Enums.Enums.Currency;
using EducationApp.DataAccessLayer.Entities.Base;

namespace EducationApp.DataAccessLayer.Entities
{
    public class OrderItem: BaseEntity
    {
        public double Amount { get; set; }
        public Currency Currency { get; set; }
        public long PrintingEditionId { get; set; }
        public virtual PrintingEdition PrintingEdition { get; set; }
        public int Count { get; set; }
        public long OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
