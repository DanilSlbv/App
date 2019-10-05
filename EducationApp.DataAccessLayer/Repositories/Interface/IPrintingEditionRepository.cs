using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Type = EducationApp.DataAccessLayer.Entities.Enums.Enums.Type;
using Currency = EducationApp.DataAccessLayer.Entities.Enums.Enums.Currency;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public  interface IPrintingEditionRepository:IBaseEFRepository<PrintingEdition>
    {
        Task<List<PrintingEdition>> GetAllAsync();
        Task<List<PrintingEdition>> GetByPriceAsync(float minPrice, float maxPrice);
        Task<List<PrintingEdition>> GetByTypeAsync(Type type);
        Task<PrintingEdition> GetByNameAsync(string name);
        Task<List<PrintingEdition>> GetByCurrencyAsync(Currency currency);
        Task<List<PrintingEdition>> SortByPriceAscendingAsync();
        Task<List<PrintingEdition>> SortByPriceDescendingAsync();
        Task RemoveAsync(int printingEditionId);
        Task<List<string>> GetNamesById(int printingEditionId);
    }
}
