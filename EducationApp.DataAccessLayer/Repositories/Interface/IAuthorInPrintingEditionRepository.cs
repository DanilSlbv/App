using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Models.Author;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IAuthorInPrintingEditionRepository:IBaseEFRepository<AuthorInPrintingEditons>
    {
        Task<List<AuthorInPrintingEditons>> GetAllAsync();
        Task<List<AuthorInPrintingEditons>> GetAuthorsByPrintingEditionId(int printingEditionId);
        Task<List<AuthorInPrintingEditons>> GetPrintingEditionsByAuthorIdAsync(int authorId);
    }
}
