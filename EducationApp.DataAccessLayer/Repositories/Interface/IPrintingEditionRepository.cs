using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Type = EducationApp.DataAccessLayer.Entities.Enums.Type;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public  interface IPrintingEditionRepository:IBaseEFRepository<PrintingEdition>
    {
        Task<List<PrintingEdition>> GetByPriceAsync(float minPrice, float maxPrice);
        Task<List<PrintingEdition>> GetByTypeAsync(Type type);
        Task<List<PrintingEdition>> SortByPriceAscendingAsync();
        Task<List<PrintingEdition>> SortByPriceDescendingAsync();
    }
}
