using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Entities.Enums;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class PrintingEditionRepository : IPrintingEditionRepository
    {
        private readonly IBaseEFRepository<PrintingEdition> _baseEFRepository;
        private readonly ApplicationContext _context;

        public PrintingEditionRepository(ApplicationContext applicationContext,IBaseEFRepository<PrintingEdition> baseEFRepository)
        {
            _context = applicationContext;
            _baseEFRepository = baseEFRepository;
        }

        public async Task<List<PrintingEdition>> GetAllAsync()
        {
            return await _baseEFRepository.GetAllAsync();
        }

        public async Task<PrintingEdition> GetByIdAsync(string id)
        {
            return await _baseEFRepository.GetByIdAsync(id);
        }

        public async Task<List<PrintingEdition>> GetItemsByPriceAsync(float min,float max)
        {
            var result =  _context.PrintingEditions.Where(x => x.Price > min && x.Price < max).ToList();
            return result;
        }
        public async Task<List<PrintingEdition>>GetItemsByTypeAsync(TypeEnumEntity type)
        {
            var result = _context.PrintingEditions.Where(x => x.type==type).ToList();
            return result;
        }
        public async Task<List<PrintingEdition>> SortItemsByPriceAscAsync()
        {
            var result = _context.PrintingEditions.OrderBy(x=>x.Price).ToList();
            return result;
        }
        public async Task<List<PrintingEdition>> SortItemsByPriceDescAsync()
        {
            var result = _context.PrintingEditions.OrderByDescending(x => x.Price).ToList();
            return result;
        }

        public async Task AddItemAsync(PrintingEdition printingEdition)
        {
             await _context.PrintingEditions.AddAsync(printingEdition);
        }

        public async Task DeleteItemAsync(string id)
        {
            await _baseEFRepository.DeleteItemAsync(id);
        }

        public async Task EditItemAsync(PrintingEdition printingEdition)
        {
            await _baseEFRepository.EditItemAsync(printingEdition);
        }
    }
}
