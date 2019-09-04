using System;

namespace EducationApp.DataAcessLayer.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime Date { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}
