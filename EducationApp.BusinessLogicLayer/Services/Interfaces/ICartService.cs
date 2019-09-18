using EducationApp.BusinessLogicLayer.Models.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface ICartService
    {
        string Charge(string stripeEmail, string stripeToken, OrderItemModel orderItemModel);
    }
}
