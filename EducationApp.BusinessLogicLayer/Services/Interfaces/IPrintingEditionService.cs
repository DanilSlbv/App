using EducationApp.BusinessLogicLayer.Models.Enums;
using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        Task<PrintingEditionModel> GetAllAsync();
        Task<PrintingEditionModelItem> GetByIdAsync(string id);
        Task<PrintingEditionModel> GetByPriceAsync(float minPrice, float maxPrice);
        Task<PrintingEditionModel> GetByTypeAsync(Type type);
        Task<PrintingEditionModel> SortByPriceAscendingAsync();
        Task<PrintingEditionModel> SortByPriceDescendingAsync();
        Task AddAsync(PrintingEditionModelItem printingEditionItemModel);
        Task DeleteAsync(string id);
        Task EditAsync(PrintingEditionModelItem printingEditionItemModel);

    }
}
