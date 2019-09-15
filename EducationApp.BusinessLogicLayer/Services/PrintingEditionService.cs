using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interface;
using System.Threading.Tasks;
using System.Linq;
using System;
using EducationApp.BusinessLogicLayer.Models.Enums;
using System.Collections.Generic;
using EducationApp.DataAccessLayer.Entities.Enums;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IPrintingEditionRepository _printingEditionRepository;
        public PrintingEditionService(IPrintingEditionRepository printingEditionRepository)
        {
            _printingEditionRepository = printingEditionRepository;
        }

        public async Task AddItemAsync(PrintingEditionItemModel printingEditionItemModel)
        {
            var printingEdition = new PrintingEdition()
            {
                Id = printingEditionItemModel.Id,
                Name = printingEditionItemModel.Name,
                Description = printingEditionItemModel.Description,
                Price = printingEditionItemModel.Price,
                IsRemoved = printingEditionItemModel.IsRemoved,
                Currency = (CurrencyEnumEntity)printingEditionItemModel.currency,
                Status = (StatusEnumEntity)printingEditionItemModel.status,
                Type = (TypeEnumEntity)printingEditionItemModel.type
            };
            await _printingEditionRepository.AddItemAsync(printingEdition);
        }

        public async Task DeleteItemAsync(string id)
        {
            await _printingEditionRepository.DeleteItemAsync(id);
        }

        public async Task EditItemAsync(PrintingEditionItemModel printingEditionItemModel)
        {
            var printingEdition = new PrintingEdition()
            {
                Id = printingEditionItemModel.Id,
                Name = printingEditionItemModel.Name,
                Description = printingEditionItemModel.Description,
                Price = printingEditionItemModel.Price,
                IsRemoved = printingEditionItemModel.IsRemoved,
                Currency = (CurrencyEnumEntity)printingEditionItemModel.currency,
                Status = (StatusEnumEntity)printingEditionItemModel.status,
                Type = (TypeEnumEntity)printingEditionItemModel.type
            };
            await _printingEditionRepository.EditItemAsync(printingEdition);
        }

        public async Task<List<PrintingEditionItemModel>> GetAllAsync()
        {
            List<PrintingEdition> printingEditions = await _printingEditionRepository.GetAllAsync();
            var printingEditionmodel = new PrintingEditionModel();
            foreach(var i in printingEditions)
            {
                printingEditionmodel.Items.Add(new PrintingEditionItemModel(i));
            }
            return printingEditionmodel.Items;
        }

        public async Task<PrintingEditionItemModel> GetByIdAsync(string id)
        {
            var printingEditionItemModel = new PrintingEditionItemModel( await _printingEditionRepository.GetByIdAsync(id));
            return printingEditionItemModel;
        }

        public async Task<List<PrintingEditionItemModel>> GetItemsByPriceAsync(float min, float max)
        {
            List<PrintingEdition> printingEditions = await _printingEditionRepository.GetItemsByPriceAsync(min, max);
            var printingEditionModel = new PrintingEditionModel();
            foreach(var i in printingEditions)
            {
                printingEditionModel.Items.Add(new PrintingEditionItemModel(i));
            }
            return printingEditionModel.Items;
        }

        public async Task<List<PrintingEditionItemModel>> GetItemsByTypeAsync(TypeModel type)
        {
            List<PrintingEdition> printingEditions = await _printingEditionRepository.GetItemsByTypeAsync((TypeEnumEntity)type);
            PrintingEditionModel printingEditionModel = new PrintingEditionModel();
            foreach (var i in printingEditions)
            {
                printingEditionModel.Items.Add(new PrintingEditionItemModel(i));
            }
            return printingEditionModel.Items;
        }

        public async Task<List<PrintingEditionItemModel>> SortItemsByPriceAscAsync()
        {
            List<PrintingEdition> printingEditions = await _printingEditionRepository.SortItemsByPriceAscAsync();
            PrintingEditionModel printingEditionModel = new PrintingEditionModel();
            foreach (var i in printingEditions)
            {
                printingEditionModel.Items.Add(new PrintingEditionItemModel(i));
            }
            return printingEditionModel.Items;
        }

        public async Task<List<PrintingEditionItemModel>> SortItemsByPriceDescAsync()
        {
            List<PrintingEdition> printingEditions = await _printingEditionRepository.SortItemsByPriceAscAsync();
            PrintingEditionModel printingEditionModel = new PrintingEditionModel();
            foreach (var i in printingEditions)
            {
                printingEditionModel.Items.Add(new PrintingEditionItemModel(i));
            }
            return printingEditionModel.Items;
        }
    }
}
