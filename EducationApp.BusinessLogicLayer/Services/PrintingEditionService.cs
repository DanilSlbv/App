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
        public PrintingEditionService(IPrintingEditionRepository printingEditionRepository)
        {
            _printingEditionRepository = printingEditionRepository;
        }

        public async Task AddAsync(PrintingEditionModelItem printingEditionItemModel)
        {
            var printingEdition = new PrintingEdition()
            {
                Id = printingEditionItemModel.Id,
                Name = printingEditionItemModel.Name,
                Description = printingEditionItemModel.Description,
                Price = printingEditionItemModel.Price,
                IsRemoved = printingEditionItemModel.IsRemoved,
                Currency = (Currency)printingEditionItemModel.currency,
                Status = (Status)printingEditionItemModel.status,
                Type = (Type)printingEditionItemModel.type
            };
            await _printingEditionRepository.AddAsync(printingEdition);
        }

        public async Task DeleteAsync(string id)
        {
            await _printingEditionRepository.DeleteAsync(id);
        }

        public async Task EditAsync(PrintingEditionModelItem printingEditionItemModel)
        {
            var printingEdition = new PrintingEdition()
            {
                Id = printingEditionItemModel.Id,
                Name = printingEditionItemModel.Name,
                Description = printingEditionItemModel.Description,
                Price = printingEditionItemModel.Price,
                IsRemoved = printingEditionItemModel.IsRemoved,
                Currency = (Currency)printingEditionItemModel.currency,
                Status = (Status)printingEditionItemModel.status,
                Type = (Type)printingEditionItemModel.type
            };
            await _printingEditionRepository.EditAsync(printingEdition);
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
