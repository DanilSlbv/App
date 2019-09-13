using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public  interface IPrintingEditionRepository:IBaseEFRepository<PrintingEdition>
    {
        Task<List<PrintingEdition>> GetItemsByPriceAsync(float min, float max);
        Task<List<PrintingEdition>> GetItemsByTypeAsync(TypeEnumEntity type);
        Task<List<PrintingEdition>> SortItemsByPriceAscAsync();
        Task<List<PrintingEdition>> SortItemsByPriceDescAsync();
    }
}
