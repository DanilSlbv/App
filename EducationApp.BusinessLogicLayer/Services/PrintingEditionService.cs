using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interface;
using System.Threading.Tasks;
using Type = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;
using StatusConvert = EducationApp.DataAccessLayer.Entities.Enums.Enums.Status;
using CurrencyConvert = EducationApp.DataAccessLayer.Entities.Enums.Enums.Currency;
using TypeConvert = EducationApp.DataAccessLayer.Entities.Enums.Enums.Type;


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

        public async Task<bool> AddAsync(EditPrintingEditionModelItem editPrintingEditionModelItem)
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

        public async Task<bool> EditAsync(EditPrintingEditionModelItem editPrintingEditionModelItem)
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

        public async Task<PrintingEditionModel> GetAllAsync()
        {
            var printingEditions = await _printingEditionRepository.GetAllAsync();
            var printingEditionmodel = new PrintingEditionModel();
            foreach(var edition in printingEditions)
            {
                printingEditionmodel.Items.Add(new PrintingEditionModelItem(edition));
            }
            return printingEditionmodel;
        }

        public async Task<PrintingEditionModelItem> GetByIdAsync(int id)
        {
            var printingEditionItemModel = new PrintingEditionModelItem( await _printingEditionRepository.GetByIdAsync(id));
            return printingEditionItemModel;
        }

        public async Task<PrintingEditionModel> GetByPriceAsync(float minPrice, float maxPrice)
        {
            var printingEditions = await _printingEditionRepository.GetByPriceAsync(minPrice, maxPrice);
            if (printingEditions == null)
            {
                return null;
            }
            var printingEditionModel = new PrintingEditionModel();
            foreach(var edition in printingEditions)
            {
                printingEditionModel.Items.Add(new PrintingEditionModelItem(edition));
            }
            return printingEditionModel;
        }


        public async Task<PrintingEditionModel> GetByTypeAsync(Type type)
        {
            var printingEditions = await _printingEditionRepository.GetByTypeAsync((TypeConvert)type);
            if (printingEditions == null)
            {
                return null;
            }
            var printingEditionModel = new PrintingEditionModel();
            foreach (var edition in printingEditions)
            {
                printingEditionModel.Items.Add(new PrintingEditionModelItem(edition));
            }
            return printingEditionModel;
        }

        public async Task<PrintingEditionModel> SortByPriceAscendingAsync()
        {
            var printingEditions = await _printingEditionRepository.SortByPriceAscendingAsync();
            if (printingEditions == null)
            {
                return null;
            }
            var printingEditionModel = new PrintingEditionModel();
            foreach (var edition in printingEditions)
            {
                printingEditionModel.Items.Add(new PrintingEditionModelItem(edition));
            }
            return printingEditionModel;
        }

        public async Task<PrintingEditionModel> SortByPriceDescendingAsync()
        {
            var printingEditions = await _printingEditionRepository.SortByPriceDescendingAsync();
            if (printingEditions == null)
            {
                return null;
            }
            var printingEditionModel = new PrintingEditionModel();
            foreach (var printingEdition in printingEditions)
            {
                printingEditionModel.Items.Add(new PrintingEditionModelItem(printingEdition));
            }
            return printingEditionModel;
        }
    }
}
