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
        public List<PrintingType> PrintingType { get; set; }
        public List<PrintingEditionModelItem> PrintingEditions { get; set; } 
        public double OrderAmount { get; set; }
        public OrdersWithOrderItemsModelItem()
        {
            PrintingType = new List<PrintingType>();
            PrintingEditions = new List<PrintingEditionModelItem>();
        }
    }
}
