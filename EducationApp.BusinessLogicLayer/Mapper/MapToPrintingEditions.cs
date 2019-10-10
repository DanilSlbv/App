using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.DataAccessLayer.Entities;
using CurrencyConvert = EducationApp.DataAccessLayer.Entities.Enums.Enums.Currency;
using StatusConvert = EducationApp.DataAccessLayer.Entities.Enums.Enums.Status;
using AscDescConvert = EducationApp.DataAccessLayer.Entities.Enums.Enums.AscendingDescending;
using PrintingTypeConvert = EducationApp.DataAccessLayer.Entities.Enums.Enums.Type;
using PrintingType = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;
using Currency = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Currency;
using Status = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Status;
using PrintingEditionFilter = EducationApp.DataAccessLayer.Models.Filters.PrintingEditionFilterModel;
using PrintingEditionFilterModel = EducationApp.BusinessLogicLayer.Models.Filters.PrintingEditionFilterModel;

namespace EducationApp.BusinessLogicLayer.Mapper
{
    public static class MapToPrintingEditions
    {
        public static PrintingEdition MapToPrintingEdition(PrintingEditionModelItem printingEditionModelItem)
        {
            return new PrintingEdition
            {
                Name = printingEditionModelItem.Name,
                Description = printingEditionModelItem.Description,
                Price = printingEditionModelItem.Price,
                Currency = (CurrencyConvert)printingEditionModelItem.Currency,
                Type = (PrintingTypeConvert)printingEditionModelItem.Type
            };
        }

        public static PrintingEditionFilter MapToPrintingEditionFilter(PrintingEditionFilterModel filterModel)
        {
            return new PrintingEditionFilter
            {
                maxPrice = filterModel.MaxPrice,
                minPrice = filterModel.MinPrice,
                SortByCurrency = (CurrencyConvert)filterModel.Currency,
                SortByPrice = (AscDescConvert)filterModel.Price,
                SortByPrintingType = (PrintingTypeConvert)filterModel.Type
            };
        }
        public static PrintingEditionModelItem MapToPrintingEditionModelItem(PrintingEdition printingEdition)
        {
            return new PrintingEditionModelItem
            {
                Id = printingEdition.Id,
                Name = printingEdition.Name,
                Description = printingEdition.Description,
                Price = printingEdition.Price,
                Currency = (Currency)printingEdition.Currency,
                Type = (PrintingType)printingEdition.Type
            };
        }

        public static PrintingEditionsWithAuthor MapToPrintingEditionsWithAuthor(AuthorInPrintingEditons printingEditions)
        {
            return new PrintingEditionsWithAuthor
            {
                Id = printingEditions.PrintingEdition.Id,
                Name = printingEditions.PrintingEdition.Name,
                AuthorName = printingEditions.Author.Name,
                Currency = (Currency)printingEditions.PrintingEdition.Currency,
                Type = (PrintingType)printingEditions.PrintingEdition.Type,
                Price = printingEditions.PrintingEdition.Price,
                Description = printingEditions.PrintingEdition.Description
            };
        }

    }
}
