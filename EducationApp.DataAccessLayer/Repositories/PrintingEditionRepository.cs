using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Type = EducationApp.DataAccessLayer.Entities.Enums.Enums.Type;
using Currency = EducationApp.DataAccessLayer.Entities.Enums.Enums.Currency;


namespace EducationApp.DataAccessLayer.Repositories
{
    public class PrintingEditionRepository : BaseEFRepository<PrintingEdition>, IPrintingEditionRepository
    {
        private readonly ApplicationContext _applicationContext;
        public PrintingEditionRepository(ApplicationContext applicationContext):base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<List<PrintingEdition>> GetAllAsync()
        {
            return await _applicationContext.PrintingEditions.Where(x => x.IsRemoved == false).ToListAsync();
        }

        public async Task<List<PrintingEdition>> GetByPriceAsync(float minPrice,float maxPrice)
        {
            return await _applicationContext.PrintingEditions.Where(x =>x.Price>=minPrice && x.Price<=minPrice).ToListAsync();
        }

        public async Task<List<PrintingEdition>>GetByTypeAsync(Type type)
        {
            return await _applicationContext.PrintingEditions.Where(x => x.Type==type).ToListAsync();
        }

        public async Task<List<PrintingEdition>> GetByCurrencyAsync(Currency currency)
        {
            return await _applicationContext.PrintingEditions.Where(x => x.Currency == currency).ToListAsync();
        }

        public async Task<PrintingEdition> GetByNameAsync(string name)
        {
            var result = await _applicationContext.PrintingEditions.FindAsync(name);
            return result;
        }

        public async Task<List<PrintingEdition>> SortByPriceAscendingAsync()
        {
            var result =await _applicationContext.PrintingEditions.OrderBy(x=>x.Price).ToListAsync();
            return result;
        }

        public async Task<List<PrintingEdition>> SortByPriceDescendingAsync()
        {
            var result =await _applicationContext.PrintingEditions.OrderByDescending(x => x.Price).ToListAsync();
            return result;
        }

        public async Task RemoveAsync(int printingEditionId)
        {
            var printingEdition =  _applicationContext.PrintingEditions.FirstOrDefault(x=>x.Id==printingEditionId);
            printingEdition.IsRemoved = true;
            _applicationContext.PrintingEditions.Update(printingEdition);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<List<string>> GetNamesById(int printingEditionId)
        {
            return await _applicationContext.PrintingEditions.Where(x => x.Id == printingEditionId).Select(x => x.Name).ToListAsync();
        }
    }
}
