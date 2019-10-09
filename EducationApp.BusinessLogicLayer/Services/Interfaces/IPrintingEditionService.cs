using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using System.Threading.Tasks;
using Type = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;
using AscendingDescending = EducationApp.BusinessLogicLayer.Models.Enums.Enums.AscendingDescending;
using Currency = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Currency;
using EducationApp.BusinessLogicLayer.Models.Pagination;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        Task<PaginationModel<PrintingEditionsWithAuthor>> GetAllWithAuthorAsync(int page, Type type, AscendingDescending price, Currency currency, float minPrice, float maxPrice);
        Task<PrintingEditionsWithAuthor> GetByIdAsync(int id);
        Task<bool> AddAsync(PrintingEditionModelItem editPrintingEditionModelItem);
        Task<bool> RemoveAsync(int id);
        Task<bool> EditAsync(PrintingEditionModelItem editPrintingEditionModelItem);
    }
}
