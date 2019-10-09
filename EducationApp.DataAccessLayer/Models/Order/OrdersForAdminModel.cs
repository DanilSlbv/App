using System;
using System.Collections.Generic;


namespace EducationApp.DataAccessLayer.Models.Order
{
    public class OrdersForAdminModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserEmail { get; set; }
        public List<string> PrintingType { get; set; }
        public List<string> PrintingTitle { get; set; }
        public double OrderAmount { get; set; }
    }
}
