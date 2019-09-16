using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Entities.Enums;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class PrintingEditionRepository : IPrintingEditionRepository,IBaseEFRepository<PrintingEdition>
    {
        private readonly ApplicationContext _context;

        public PrintingEditionRepository(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task<List<PrintingEdition>> GetAllAsync()
        {
            return await GetAllAsync();
        }

        public async Task<PrintingEdition> GetByIdAsync(string id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<List<PrintingEdition>> GetByPriceAsync(float minPrice,float maxPrice)
        {
            var result =await  _context.PrintingEditions.Where(x => x.Price > minPrice && x.Price < maxPrice).ToListAsync();
            return result;
        }
        public async Task<List<PrintingEdition>>GetByTypeAsync(Type type)
        {
            var result = await _context.PrintingEditions.Where(x => x.Type==type).ToListAsync();
            return result;
        }
        public async Task<List<PrintingEdition>> SortByPriceAscendingAsync()
        {
            var result =await _context.PrintingEditions.OrderBy(x=>x.Price).ToListAsync();
            return result;
        }
        public async Task<List<PrintingEdition>> SortByPriceDescendingAsync()
        {
            var result =await _context.PrintingEditions.OrderByDescending(x => x.Price).ToListAsync();
            return result;
        }

        public async Task AddAsync(PrintingEdition printingEdition)
        {
             await _context.PrintingEditions.AddAsync(printingEdition);
        }

        public async Task DeleteAsync(string id)
        {
            await DeleteAsync(id);
        }

        public async Task EditAsync(PrintingEdition printingEdition)
        {
            await EditAsync(printingEdition);
        }
    }
}
