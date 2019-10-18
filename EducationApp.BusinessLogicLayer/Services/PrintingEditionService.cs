using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Repositories.Interface;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Models.Response;
using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.BusinessLogicLayer.Common.Constants;
using EducationApp.BusinessLogicLayer.Models.Filters;
using EducationApp.BusinessLogicLayer.Common.Extensions;
using System;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IPrintingEditionRepository _printingEditionRepository;
        private readonly IAuthorInPrintingEditionRepository _authorInPrintingEditionRepository;
        public PrintingEditionService(IPrintingEditionRepository printingEditionRepository, IAuthorInPrintingEditionRepository authorInPrintingEditionRepository)
        {
            _printingEditionRepository = printingEditionRepository;
            _authorInPrintingEditionRepository = authorInPrintingEditionRepository;
        }

        public async Task<BaseModel> CreateAsync(PrintingEditionsWithAuthor createPrintingEditionModelItem)
        {
            var baseModel = new BaseModel();
            if (await _printingEditionRepository.CreateAsync(Mapper.PrintingEditionMapper.MapToPrintingEdition(createPrintingEditionModelItem)))
            {
                return baseModel;
            }
            var printingEditionId = await _printingEditionRepository.GetIdByNameAsync(createPrintingEditionModelItem.Name);
            foreach (var author in createPrintingEditionModelItem.Authors)
            {
                if (!await _authorInPrintingEditionRepository.CreateAsync(Mapper.AuthorInPrintingEditionMapper.MapToAuthorInPrintingEditions(0, printingEditionId, author.Id)))
                {
                    break;
                }
            }
            baseModel.Errors.Add(Constants.Errors.ErrorToUpdate);
            return baseModel;
        }

        public async Task<BaseModel> RemoveAsync(int id)
        {
            var baseModel = new BaseModel();
            var printingEdition = await _printingEditionRepository.GetByIdAsync(id);
            if (printingEdition == null)
            {
                baseModel.Errors.Add(Constants.Errors.NotFount);
                return baseModel;
            }
            printingEdition.IsRemoved = true;
            if (await _printingEditionRepository.EditAsync(printingEdition))
            {
                return baseModel;
            }
            baseModel.Errors.Add(Constants.Errors.ErrorToUpdate);
            return baseModel;
        }

        public async Task<BaseModel> EditAsync(PrintingEditionsWithAuthor editPrintingEditionModelItem)
        {
            var baseModel = new BaseModel();
            if (!await _printingEditionRepository.EditAsync(Mapper.PrintingEditionMapper.MapToPrintingEdition(editPrintingEditionModelItem)))
            {
                baseModel.Errors.Add(Constants.Errors.ErrorToUpdate);
                return baseModel;
            }
            foreach(var author in editPrintingEditionModelItem.Authors)
            {
                if (!await _authorInPrintingEditionRepository.ChekcIfAuthorExistAsync(editPrintingEditionModelItem.Id, author.Id))
                {
                    await _authorInPrintingEditionRepository.CreateAsync(
                        Mapper.AuthorInPrintingEditionMapper.MapToAuthorInPrintingEditions(0, editPrintingEditionModelItem.Id, author.Id));
                }
            }
            return baseModel;
        }

        public async Task<ResponseModel<PrintingEditionsWithAuthor>> GetAllWithAuthorAsync(int page, PrintingEditionFilterModel filterModel)
        {
            var printingEditionsWithAuthor = new ResponseModel<PrintingEditionsWithAuthor>();
            var sortedItems = await _printingEditionRepository.SortWithAuthorsAsync(page, Mapper.PrintingEditionMapper.MapToPrintingEditionFilter(filterModel));
            foreach (var printingEdition in sortedItems.Items)
            {
                printingEdition.Price = CurrencyExchangeExtension.CurrencyConvert(printingEdition.Price, filterModel.Currency);
                printingEditionsWithAuthor.Items.Add(Mapper.PrintingEditionMapper.MapToPrintingEditionsWithAuthor(printingEdition));
            }
            printingEditionsWithAuthor.TotalItems = sortedItems.ItemsCount;
            return printingEditionsWithAuthor;
        }

        public async Task<PrintingEditionsWithAuthor> GetByIdAsync(int id)
        {
            return Mapper.PrintingEditionMapper.MapToPrintingEditionsWithAuthor(await _printingEditionRepository.GetWithAuthorsById(id));
        }

    }
}
