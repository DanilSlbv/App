using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Models.Response;
using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.BusinessLogicLayer.Models.Filters;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        Task<ResponseModel<PrintingEditionsWithAuthor>> GetAllWithAuthorAsync(int page, PrintingEditionFilterModel filterModel);
        Task<PrintingEditionsWithAuthor> GetByIdAsync(int id);
        Task<BaseModel> CreateAsync(PrintingEditionModelItem editPrintingEditionModelItem);
        Task<BaseModel> RemoveAsync(int id);
        Task<BaseModel> EditAsync(PrintingEditionModelItem editPrintingEditionModelItem);
    }
}
