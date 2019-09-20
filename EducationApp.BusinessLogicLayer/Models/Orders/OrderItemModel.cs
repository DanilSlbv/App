using EducationApp.BusinessLogicLayer.Models.Base;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class OrderItemModel:BaseModel
    {
        public List<OrderItemModelItem> Items { get; set; }
        public OrderItemModel()
        {
            Items = new List<OrderItemModelItem>();
        }
    }
}
