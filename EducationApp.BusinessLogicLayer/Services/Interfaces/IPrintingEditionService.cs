using EducationApp.BusinessLogicLayer.Models.Enums;
using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        Task<List<PrintingEditionItemModel>> GetAllAsync();
        Task<PrintingEditionItemModel> GetByIdAsync(string id);
        Task<List<PrintingEditionItemModel>> GetItemsByPriceAsync(float min, float max);
        Task<List<PrintingEditionItemModel>> GetItemsByTypeAsync(TypeModel type);
        Task<List<PrintingEditionItemModel>> SortItemsByPriceAscAsync();
        Task<List<PrintingEditionItemModel>> SortItemsByPriceDescAsync();
        Task AddItemAsync(PrintingEditionItemModel printingEditionItemModel);
        Task DeleteItemAsync(string id);
        Task EditItemAsync(PrintingEditionItemModel printingEditionItemModel);

    }
}
