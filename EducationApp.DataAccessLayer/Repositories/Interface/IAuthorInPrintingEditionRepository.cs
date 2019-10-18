using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Models.Authors;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IAuthorInPrintingEditionRepository:IBaseEFRepository<AuthorInPrintingEditons>
    {
        Task<List<long>> GetIdByPrintingEditionIdAsync(long printingEditionId);
        Task<List<AuthorInPrintingEditons>> GetAllAsync();
        Task<bool> ChekcIfAuthorExistAsync(long printingEditionId, long authorId);
    }
}
