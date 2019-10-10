using EducationApp.DataAccessLayer.Entities;
using System.Threading.Tasks;
using EducationApp.DataAccessLayer.Models.Response;
using EducationApp.DataAccessLayer.Models.Filters;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public  interface IPrintingEditionRepository:IBaseEFRepository<PrintingEdition>
    {
        Task<AuthorInPrintingEditons> GetWithAuthorsById(int id);
        Task<ResponseModel<AuthorInPrintingEditons>> SortWithAuthorsAsync(int page, PrintingEditionFilterModel filterModel);
    }
}
