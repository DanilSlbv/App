using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Entities.Enums;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class PrintingEditionRepository : BaseEFRepository<PrintingEdition>, IPrintingEditionRepository
    {
        private readonly ApplicationContext _applicationContext;
        public PrintingEditionRepository(ApplicationContext applicationContext):base(applicationContext)
        {
            _applicationContext = applicationContext;
        }


        public async Task<List<PrintingEdition>> GetByPriceAsync(float minPrice,float maxPrice)
        {
            var result = await _context.PrintingEditions.Where(x =>x.Price>=minPrice && x.Price<=minPrice).ToListAsync();
            return result;
        }
        public async Task<List<PrintingEdition>>GetByTypeAsync(Type type)
        {
            var result = await _context.PrintingEditions.Where(x => x.Type==type).ToListAsync();
            return result;
        }
        public async Task<PrintingEdition> GetByNameAsync(string name)
        {
            var result = await _context.PrintingEditions.FindAsync(name);
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
    }
}
