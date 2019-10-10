using EducationApp.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Mapper
{
    public static class MapToPayments
    {
        public static Payment MapToPayment(string transactionId)
        {
            return new Payment
            {
                TransactionId = transactionId
            };
        }
    }
}
