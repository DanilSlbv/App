using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class ChargeModel
    {
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }
        [DataType(DataType.CreditCard)]
        public string Token { get; set; }
        public long Amount { get; set; }
    }
}
