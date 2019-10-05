using Type=EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;
using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using System.Collections.Generic;
using System;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class UserOrdersInfoModelItem
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Type> Type { get; set; }
        public List<string> Title { get; set; }
        public double OrderAmount { get; set; }
    }
}
