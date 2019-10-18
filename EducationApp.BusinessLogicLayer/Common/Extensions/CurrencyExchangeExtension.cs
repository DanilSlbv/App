using System.Collections.Generic;
using Currency = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Currency;

namespace EducationApp.BusinessLogicLayer.Common.Extensions
{
    public static class CurrencyExchangeExtension
    {
        public static float CurrencyConvert(this float price, Currency currency)
        {
            Dictionary<Currency, double> CurrencyAndValues = new Dictionary<Currency, double>()
            {
                {Currency.USD, 1 },
                {Currency.EUR, 0.91 },
                {Currency.GBP, 0.81 },
                {Currency.CHF, 0.99 },
                {Currency.JPY, 106.99 },
                {Currency.UAH, 24.60 }
            };
            var value = CurrencyAndValues.GetValueOrDefault(currency);
            return price * (float)value;
        }
    }
}
