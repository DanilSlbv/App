using EducationApp.BusinessLogicLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class FullOrderInfoModelItem:BaseModel
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string TransactinId { get; set; }
        public string userEmail { get; set; }
    }
}
