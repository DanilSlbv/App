

namespace EducationApp.DataAcessLayer.Entities
{
   public  class OrderItem
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public enum Currency {  }
        public int PrintingEditionsId { get; set; }
        public PrintingEdition PrintingEditons { get; set; }
        public int Count { get; set; }
    }
}
