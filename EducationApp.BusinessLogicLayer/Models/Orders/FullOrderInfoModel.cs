using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class FullOrderInfoModel
    {
        public List<FullOrderInfoModelItem> items;
        public FullOrderInfoModel()
        {
            items= new List<FullOrderInfoModelItem>();
        }
    }
}
