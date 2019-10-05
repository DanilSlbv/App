using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class FullOrdersInfoModel
    {
        public List<FullOrdersInfoModelItem> Items { get; set; }
        public FullOrdersInfoModel()
        {
            Items = new List<FullOrdersInfoModelItem>();
        }
    }
}
