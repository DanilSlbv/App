using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Repositories.Interface;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Helpers;
using EducationApp.BusinessLogicLayer.Models.Response;
using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.BusinessLogicLayer.Common.Constants;
using EducationApp.BusinessLogicLayer.Models.Filters;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IPrintingEditionRepository _printingEditionRepository;
        public PrintingEditionService(IPrintingEditionRepository printingEditionRepository)
        {
            _printingEditionRepository = printingEditionRepository;
        }

        public async Task<BaseModel> CreateAsync(PrintingEditionModelItem createPrintingEditionModelItem)
        {
            var baseModel = new BaseModel();
            if(await _printingEditionRepository.CreateAsync(Mapper.MapToPrintingEditions.MapToPrintingEdition(createPrintingEditionModelItem)))
            {
                return baseModel;
            }
            baseModel.Errors.Add(Constants.Errors.ErrorToUpdate);
            return baseModel;
        }

        public async Task<BaseModel> RemoveAsync(int id)
        {
            var baseModel = new BaseModel();
            if (await _printingEditionRepository.GetByIdAsync(id) != null)
            {
                baseModel.Errors.Add(Constants.Errors.NotFount);
                return baseModel;
            }
            if(await _printingEditionRepository.RemoveAsync(id))
            {
                return baseModel;
            }
            baseModel.Errors.Add(Constants.Errors.ErrorToUpdate);
            return baseModel;
        }

        public async Task<BaseModel> EditAsync(PrintingEditionModelItem editPrintingEditionModelItem)
        {
            var baseModel = new BaseModel();
            var printingEdition = await _printingEditionRepository.GetByIdAsync(editPrintingEditionModelItem.Id);
            if (printingEdition == null)
            {
                baseModel.Errors.Add(Constants.Errors.NotFount);
                return baseModel;
            };
            if(await _printingEditionRepository.EditAsync(Mapper.MapToPrintingEditions.MapToPrintingEdition(editPrintingEditionModelItem)))
            {
                return baseModel;
            }
            baseModel.Errors.Add(Constants.Errors.ErrorToUpdate);
            return baseModel;
        }

        public async Task<ResponseModel<PrintingEditionsWithAuthor>> GetAllWithAuthorAsync(int page, PrintingEditionFilterModel filterModel)
        {
            var printingEditionsWithAuthor = new ResponseModel<PrintingEditionsWithAuthor>();
            var sortedItems = await _printingEditionRepository.SortWithAuthorsAsync(page, Mapper.MapToPrintingEditions.MapToPrintingEditionFilter(filterModel));
            foreach (var printingEdition in sortedItems.Items)
            {
                var currencyExchange = new CurrencyExchange();
                printingEditionsWithAuthor.Items.Add(currencyExchange.Convert(Mapper.MapToPrintingEditions.MapToPrintingEditionsWithAuthor(printingEdition), filterModel.Currency));
            }
            printingEditionsWithAuthor.TotalItems = sortedItems.ItemsCount;
            return printingEditionsWithAuthor;
        }

        public async Task<PrintingEditionsWithAuthor> GetByIdAsync(int id)
        {
            return Mapper.MapToPrintingEditions.MapToPrintingEditionsWithAuthor(await _printingEditionRepository.GetWithAuthorsById(id));
        }
    }
}
