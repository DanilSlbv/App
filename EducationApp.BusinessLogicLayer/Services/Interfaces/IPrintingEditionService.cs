using EducationApp.BusinessLogicLayer.Models.Enums;
using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using System.Collections.Generic;
using System.Threading.Tasks;
using Type = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        Task<PrintingEditionModel> GetAllAsync();
        Task<PrintingEditionModelItem> GetByIdAsync(int id);
        Task<PrintingEditionModel> GetByPriceAsync(float minPrice, float maxPrice);
        Task<PrintingEditionModel> GetByTypeAsync(Type type);
        Task<PrintingEditionModel> SortByPriceAscendingAsync();
        Task<PrintingEditionModel> SortByPriceDescendingAsync();
        Task<bool> AddAsync(EditPrintingEditionModelItem editPrintingEditionModelItem);
        Task<bool> RemoveAsync(int id);
        Task<bool> EditAsync(EditPrintingEditionModelItem editPrintingEditionModelItem);

    }
}
