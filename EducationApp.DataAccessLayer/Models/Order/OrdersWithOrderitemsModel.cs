using EducationApp.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using Type = EducationApp.DataAccessLayer.Entities.Enums.Enums.Type;

namespace EducationApp.DataAccessLayer.Models.Order
{
    public class OrdersWithOrderItemsModel
    {
        public long OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserEmail { get; set; }
        public List<Type> PrintingType { get; set; }
        public List<PrintingEdition> PrintingEditions { get; set; }
        public double OrderAmount { get; set; }
    }
}
