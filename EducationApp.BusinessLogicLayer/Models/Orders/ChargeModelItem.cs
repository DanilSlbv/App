using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class ChargeModelItem
    {
        public string StripeEmail { get; set; }
        public string StripeToken { get; set; }
        public long Amount { get; set; }
    }
}
