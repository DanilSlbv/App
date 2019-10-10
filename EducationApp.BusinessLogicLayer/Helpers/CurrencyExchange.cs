using EducationApp.BusinessLogicLayer.Common.Constants;
using EducationApp.BusinessLogicLayer.Common.Extensions;
using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using Currency = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Currency;

namespace EducationApp.BusinessLogicLayer.Helpers
{
    public class CurrencyExchange
    {
        public PrintingEditionsWithAuthor Convert(PrintingEditionsWithAuthor item,Currency currency)
        {
            if (currency == Currency.EUR)
            {
                item.Price=item.Price*(float)Constants.ExchangeRates.EUR;
            }
            if (currency == Currency.GBP)
            {
                item.Price = item.Price * (float)Constants.ExchangeRates.GBP;
            }
            if (currency == Currency.CHF)
            {
                item.Price = item.Price * (float)Constants.ExchangeRates.CHF;
            }
            if (currency == Currency.JPY)
            {
                item.Price = item.Price * (float)Constants.ExchangeRates.JPY;
            }
            if (currency == Currency.UAH)
            {
                item.Price = item.Price * (float)Constants.ExchangeRates.UAH;
            }
            return item;
        }
    }
}
