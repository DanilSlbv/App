using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using System;
using PrintingType = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class OrdersWithOrderItemsModelItem
    {
        public long OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserEmail { get; set; }
        public List<PrintingType> PrintingTypes { get; set; }
        public List<PrintingEditionsWithAuthor> PrintingEditions { get; set; } 
        public double OrderAmount { get; set; }
        public OrdersWithOrderItemsModelItem()
        {
            PrintingTypes = new List<PrintingType>();
            PrintingEditions = new List<PrintingEditionsWithAuthor>();
        }
    }
}
