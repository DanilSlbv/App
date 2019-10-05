using System;
using System.Collections.Generic;
using Type = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class FullOrdersInfoModelItem
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public List<Type> Type { get; set; }
        public List<string> Title { get; set; } 
        public double OrderAmount { get; set; }
    }
}
