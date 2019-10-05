using EducationApp.BusinessLogicLayer.Models.Base;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class OrderModel:BaseModel
    {
        public List<OrderModelItem> Items { get; set; }
        public OrderModel()
        {
            Items = new List<OrderModelItem>();
        }
    }
}
