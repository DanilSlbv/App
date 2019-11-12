using EducationApp.DataAccessLayer.Entities;
using System.Threading.Tasks;
using EducationApp.DataAccessLayer.Models.Response;
using EducationApp.DataAccessLayer.Models.Filters;
using EducationApp.DataAccessLayer.Models.PrintingEditions;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IPrintingEditionRepository : IBaseEFRepository<PrintingEdition>
    {
        Task<long> GetIdByNameAsync(string name);
        Task<PrintingEditionWithAuthorsModel> GetWithAuthorsByIdAsync(long id);
        Task<ResponseModel<PrintingEditionWithAuthorsModel>> SortWithAuthorsAsync(int page, PrintingEditionFilterModel filterModel);
    }
}
