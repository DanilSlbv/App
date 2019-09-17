using EducationApp.DataAccessLayer.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAcessLayer.AppContext;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class AuthorInPrintingEditionRepository:IAuthorInPrintingEditionRepository,IBaseEFRepository<AuthorInPrintingEditons>
    {
        private readonly ApplicationContext _context;
        public AuthorInPrintingEditionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AuthorInPrintingEditons authorInPrintingEditons)
        {
             await AddAsync(authorInPrintingEditons);
        }

        public async Task DeleteAsync(string id)
        {
            await DeleteAsync(id);
        }

        public async Task EditAsync(AuthorInPrintingEditons authorInPrintingEditons)
        {
            await EditAsync(authorInPrintingEditons);
        }

        public async Task<List<AuthorInPrintingEditons>> GetAllAsync()
        {
            var items = await GetAllAsync();
            return items;
        }

        public async Task<AuthorInPrintingEditons> GetByIdAsync(string id)
        {
            var item = await GetByIdAsync(id);
            return item;
        }
    }
}
