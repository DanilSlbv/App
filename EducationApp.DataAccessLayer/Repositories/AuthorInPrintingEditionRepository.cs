using EducationApp.DataAccessLayer.Repositories.Interface;
using System.Collections.Generic;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAcessLayer.AppContext;
using System.Threading.Tasks;
using EducationApp.DataAccessLayer.Repositories.Base;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class AuthorInPrintingEditionRepository:BaseEFRepository<AuthorInPrintingEditons>,IAuthorInPrintingEditionRepository
    {
        private readonly ApplicationContext _applicationContext;
        public AuthorInPrintingEditionRepository(ApplicationContext applicationContext) :base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<List<AuthorInPrintingEditons>> GetAuthorsByPrintingEditionId(int printingEditionId)
        {
            return await _applicationContext.AuthorInPrintingEditons.Where(x => x.PrintingEditionId == printingEditionId).ToListAsync();
        }

        public async Task<List<AuthorInPrintingEditons>> GetPrintingEditionsByAuthorIdAsync(int authorId)
        {
            return await _applicationContext.AuthorInPrintingEditons.Where(x => x.AuthorId == authorId && x.IsRemoved==false).ToListAsync();
        }
        public async Task<List<AuthorInPrintingEditons>> GetAllAsync()
        {
            return await _applicationContext.AuthorInPrintingEditons.Where(x => x.IsRemoved == false).ToListAsync();
        }
    }
}
