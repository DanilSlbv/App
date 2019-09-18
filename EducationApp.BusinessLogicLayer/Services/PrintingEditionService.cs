using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;
using Currency = EducationApp.DataAccessLayer.Entities.Enums.Currency;
using Status = EducationApp.DataAccessLayer.Entities.Enums.Status;
using Type = EducationApp.DataAccessLayer.Entities.Enums.Type;

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

        public async Task AddAsync(AddPrintingEditionModelItem addPrintingEditionModelItem)
        {
            var printingEdition = new PrintingEdition()
            {
                Name = addPrintingEditionModelItem.Name,
                Description = addPrintingEditionModelItem.Description,
                Price = addPrintingEditionModelItem.Price,
                IsRemoved = addPrintingEditionModelItem.IsRemoved,
                Currency = (Currency)addPrintingEditionModelItem.currency,
                Status = (Status)addPrintingEditionModelItem.status,
                Type = (Type)addPrintingEditionModelItem.type
            };
            await _printingEditionRepository.AddAsync(printingEdition);
        }

        public async Task DeleteAsync(string id)
        {
            await _printingEditionRepository.DeleteAsync(id);
        }

        public async Task EditAsync(EditPrintingEditionModelItem editPrintingEditionModelItem)
        {
            var printingEdition = await _printingEditionRepository.GetByIdAsync(editPrintingEditionModelItem.Id);
            if(printingEdition!=null)
            {
                printingEdition.Name = editPrintingEditionModelItem.Name;
                printingEdition.Description = editPrintingEditionModelItem.Description;
                printingEdition.Price = editPrintingEditionModelItem.Price;
                printingEdition.IsRemoved = editPrintingEditionModelItem.IsRemoved;
                printingEdition.Currency = (Currency)editPrintingEditionModelItem.currency;
                printingEdition.Status = (Status)editPrintingEditionModelItem.status;
                printingEdition.Type = (Type)editPrintingEditionModelItem.type;
                await _printingEditionRepository.EditAsync(printingEdition);
            }; 
        }

        public async Task<PrintingEditionModel> GetAllAsync()
        {
            List<PrintingEdition> printingEditions = await _printingEditionRepository.GetAllAsync();
            var printingEditionmodel = new PrintingEditionModel();
            foreach(var edition in printingEditions)
            {
                printingEditionmodel.Items.Add(new PrintingEditionModelItem(edition));
            }
            return printingEditionmodel;
        }

        public async Task<PrintingEditionModelItem> GetByIdAsync(string id)
        {
            var printingEditionItemModel = new PrintingEditionModelItem( await _printingEditionRepository.GetByIdAsync(id));
            return printingEditionItemModel;
        }

        public async Task<PrintingEditionModel> GetByPriceAsync(float minPrice, float maxPrice)
        {
            List<PrintingEdition> printingEditions = await _printingEditionRepository.GetByPriceAsync(minPrice, maxPrice);
            var printingEditionModel = new PrintingEditionModel();
            foreach(var edition in printingEditions)
            {
                printingEditionModel.Items.Add(new PrintingEditionModelItem(edition));
            }
            return printingEditionModel;
        }


        public async Task<PrintingEditionModel> GetByTypeAsync(EducationApp.BusinessLogicLayer.Models.Enums.Type type)
        {
            List<PrintingEdition> printingEditions = await _printingEditionRepository.GetByTypeAsync((Type)type);
            PrintingEditionModel printingEditionModel = new PrintingEditionModel();
            foreach (var edition in printingEditions)
            {
                printingEditionModel.Items.Add(new PrintingEditionModelItem(edition));
            }
            return printingEditionModel;
        }

        public async Task<PrintingEditionModel> SortByPriceAscendingAsync()
        {
            List<PrintingEdition> printingEditions = await _printingEditionRepository.SortByPriceAscendingAsync();
            PrintingEditionModel printingEditionModel = new PrintingEditionModel();
            foreach (var edition in printingEditions)
            {
                printingEditionModel.Items.Add(new PrintingEditionModelItem(edition));
            }
            return printingEditionModel;
        }

        public async Task<PrintingEditionModel> SortByPriceDescendingAsync()
        {
            List<PrintingEdition> printingEditions = await _printingEditionRepository.SortByPriceDescendingAsync();
            PrintingEditionModel printingEditionModel = new PrintingEditionModel();
            foreach (var printingEdition in printingEditions)
            {
                printingEditionModel.Items.Add(new PrintingEditionModelItem(printingEdition));
            }
            return printingEditionModel;
        }
    }
}
