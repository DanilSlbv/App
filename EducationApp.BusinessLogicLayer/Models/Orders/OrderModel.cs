﻿using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.BusinessLogicLayer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

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
