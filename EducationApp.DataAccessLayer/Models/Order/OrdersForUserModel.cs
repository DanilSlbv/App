using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.DataAccessLayer.Models.Order
{
    public class OrdersForUserModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<string> PrintingType { get; set; }
        public List<string> PrintingTitle { get; set; }
        public double OrderAmount { get; set; }
    }
}
