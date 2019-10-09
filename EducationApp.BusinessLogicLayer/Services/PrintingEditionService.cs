using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interface;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Common.Extensions;
using Type = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;
using Currency = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Currency;
using AscendingDescending = EducationApp.BusinessLogicLayer.Models.Enums.Enums.AscendingDescending;
using StatusConvert = EducationApp.DataAccessLayer.Entities.Enums.Enums.Status;
using CurrencyConvert = EducationApp.DataAccessLayer.Entities.Enums.Enums.Currency;
using TypeConvert = EducationApp.DataAccessLayer.Entities.Enums.Enums.Type;
using AscendingDescendingConvert = EducationApp.DataAccessLayer.Entities.Enums.Enums.AscendingDescending;
using System.Collections.Generic;
using System.Linq;
using EducationApp.BusinessLogicLayer.Models.Pagination;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IPrintingEditionRepository _printingEditionRepository;
        private readonly IAuthorInPrintingEditionRepository _authorInPrintingEditionRepository;
        public PrintingEditionService(IPrintingEditionRepository printingEditionRepository,IAuthorInPrintingEditionRepository authorInPrintingEditionRepository)
        {
            _printingEditionRepository = printingEditionRepository;
            _authorInPrintingEditionRepository = authorInPrintingEditionRepository;
        }

        public async Task<bool> AddAsync(PrintingEditionModelItem editPrintingEditionModelItem)
        {
            if (editPrintingEditionModelItem == null)
            {
                return false;
            }
            var printingEdition = new PrintingEdition()
            {
                Name = editPrintingEditionModelItem.Name,
                Description = editPrintingEditionModelItem.Description,
                Price = editPrintingEditionModelItem.Price,
                IsRemoved = editPrintingEditionModelItem.IsRemoved,
                Currency = (CurrencyConvert)editPrintingEditionModelItem.Currency,
                Status = (StatusConvert)editPrintingEditionModelItem.Status,
                Type = (TypeConvert)editPrintingEditionModelItem.Type
            };
            await _printingEditionRepository.AddAsync(printingEdition);
            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            if (await _printingEditionRepository.GetByIdAsync(id) != null)
            {
                await _printingEditionRepository.RemoveAsync(id);
                return true;
            }
            return false;
        }

        public async Task<bool> EditAsync(PrintingEditionModelItem editPrintingEditionModelItem)
        {
            var printingEdition = await _printingEditionRepository.GetByIdAsync(editPrintingEditionModelItem.Id);
            if(printingEdition==null)
            {
                return false;
            };
            printingEdition.Name = editPrintingEditionModelItem.Name;
            printingEdition.Description = editPrintingEditionModelItem.Description;
            printingEdition.Price = editPrintingEditionModelItem.Price;
            printingEdition.IsRemoved = editPrintingEditionModelItem.IsRemoved;
            printingEdition.Currency = (CurrencyConvert)editPrintingEditionModelItem.Currency;
            printingEdition.Status = (StatusConvert)editPrintingEditionModelItem.Status;
            printingEdition.Type = (TypeConvert)editPrintingEditionModelItem.Type;
            await _printingEditionRepository.EditAsync(printingEdition);
            return false;
        }

        public async Task<PaginationModel<PrintingEditionsWithAuthor>> GetAllWithAuthorAsync(int page,Type type,AscendingDescending price,Currency currency,float minPrice,float maxPrice)
        {
            var printingEditionsWithAuthor = new PaginationModel<PrintingEditionsWithAuthor>();
            var sortedItems =await _printingEditionRepository.SortWithAuthorsAsync(page,(TypeConvert)type,(AscendingDescendingConvert)price, minPrice,maxPrice);
            foreach (var printingEdition in sortedItems.Items)
            {
                 printingEditionsWithAuthor.Items.Add(new PrintingEditionsWithAuthor(printingEdition));
            }
            if ((int)currency == 2)
            {
                printingEditionsWithAuthor.Items.ForEach(x => x.Price = x.Price * (float)Constants.ExchangeRates.EUR);
            }
            if ((int)currency == 3)
            {
                printingEditionsWithAuthor.Items.ForEach(x => x.Price = x.Price * (float)Constants.ExchangeRates.GBP);
            }
            if ((int)currency == 4)
            {
                printingEditionsWithAuthor.Items.ForEach(x => x.Price = x.Price * (float)Constants.ExchangeRates.CHF);
            }
            if ((int)currency == 5)
            {
                printingEditionsWithAuthor.Items.ForEach(x => x.Price = x.Price * (float)Constants.ExchangeRates.JPY);
            }
            if ((int)currency == 6)
            {
                printingEditionsWithAuthor.Items.ForEach(x => x.Price = x.Price * (float)Constants.ExchangeRates.UAH);
            }
            printingEditionsWithAuthor.TotalItems = sortedItems.ItemsCount;
            return printingEditionsWithAuthor;
        }

        public async Task<PrintingEditionsWithAuthor> GetByIdAsync(int id)
        {
            var printingEditionItemModel = new PrintingEditionsWithAuthor(await _printingEditionRepository.GetWithAuthorsById(id));
            return printingEditionItemModel;
        }
    }
}
