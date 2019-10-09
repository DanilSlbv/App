using EducationApp.DataAccessLayer.Entities;
using System.Threading.Tasks;
using Type = EducationApp.DataAccessLayer.Entities.Enums.Enums.Type;
using AscendingDescending = EducationApp.DataAccessLayer.Entities.Enums.Enums.AscendingDescending;
using EducationApp.DataAccessLayer.Models.Pagination;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public  interface IPrintingEditionRepository:IBaseEFRepository<PrintingEdition>
    {
        Task RemoveAsync(int printingEditionId);
        Task<AuthorInPrintingEditons> GetWithAuthorsById(int id);
        Task<PaginationModel<AuthorInPrintingEditons>> SortWithAuthorsAsync(int page, Type type, AscendingDescending price, float minPrice, float maxPrice);
    }
}
