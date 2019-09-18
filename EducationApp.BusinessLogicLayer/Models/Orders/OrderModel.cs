using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.BusinessLogicLayer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class OrderModel:BaseModel
    {
        public double Amount { get; set; }
        public Currency Currency { get; set; }
        public string PrintingEditionId { get; set; }
        public int Count { get; set; }
    }
}
