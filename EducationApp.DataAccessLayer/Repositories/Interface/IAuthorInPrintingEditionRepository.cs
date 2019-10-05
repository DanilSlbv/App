using System.Collections.Generic;
using System.Threading.Tasks;
using EducationApp.DataAccessLayer.Entities;
namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IAuthorInPrintingEditionRepository:IBaseEFRepository<AuthorInPrintingEditons>
    {
        Task<List<AuthorInPrintingEditons>> GetAllAsync();
        Task<List<AuthorInPrintingEditons>> GetAuthorsByPrintingEditionId(int printingEditionId);
        Task<List<AuthorInPrintingEditons>> GetPrintingEditionsByAuthorIdAsync(int authorId);
    }
}
