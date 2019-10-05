using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class UserOrdersInfoModel
    {
        public List<UserOrdersInfoModelItem> Items { get; set; }
        public UserOrdersInfoModel()
        {
            Items = new List<UserOrdersInfoModelItem>();
        }
    }
}
