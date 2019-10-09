using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using EducationApp.DataAccessLayer.Common;
using EducationApp.DataAccessLayer.Models.Pagination;
using Type = EducationApp.DataAccessLayer.Entities.Enums.Enums.Type;
using AscendingDescending = EducationApp.DataAccessLayer.Entities.Enums.Enums.AscendingDescending;
using System.Collections.Generic;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class PrintingEditionRepository : BaseEFRepository<PrintingEdition>, IPrintingEditionRepository
    {
        private readonly ApplicationContext _applicationContext;
        public PrintingEditionRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }


        public async Task RemoveAsync(int printingEditionId)
        {
            var printingEdition = _applicationContext.PrintingEditions.FirstOrDefault(x => x.Id == printingEditionId);
            printingEdition.IsRemoved = true;
            _applicationContext.PrintingEditions.Update(printingEdition);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<AuthorInPrintingEditons> GetWithAuthorsById(int id)
        {
            var printingEdition =await _applicationContext.AuthorInPrintingEditons.Include(x => x.Author).Include(y => y.PrintingEdition).Where(x=>x.PrintingEdition.Id==id).FirstOrDefaultAsync();
            return printingEdition;
        }

        public async Task<PaginationModel<AuthorInPrintingEditons>> SortWithAuthorsAsync(int page, Type bookType, AscendingDescending priceSorting, float minPrice, float maxPrice)
        {
            var resultItems = new PaginationModel<AuthorInPrintingEditons>();
            IQueryable<AuthorInPrintingEditons> printingEditions=null;
            if (bookType == Type.None)
            {
                printingEditions = _applicationContext.AuthorInPrintingEditons.Include(x => x.Author).Include(y => y.PrintingEdition).AsQueryable();
              
            }
            if (bookType != Type.None)
            {
                printingEditions =  _applicationContext.AuthorInPrintingEditons.Include(x => x.Author).Include(y => y.PrintingEdition).Where(x=>x.PrintingEdition.Type==bookType).AsQueryable();

            }
            if (minPrice>=0 || maxPrice<=10000)
            {
                printingEditions = printingEditions.Where(x => x.PrintingEdition.Price >= minPrice && x.PrintingEdition.Price <= maxPrice);
            }
            if (priceSorting == AscendingDescending.Ascending)
            {
                printingEditions = printingEditions.OrderBy(x => x.PrintingEdition.Price);
            }
            if (priceSorting == AscendingDescending.Descending)
            {
                printingEditions = printingEditions.OrderByDescending(x => x.PrintingEdition.Price);
            }
            resultItems.ItemsCount = printingEditions.Count();
            resultItems.Items =await printingEditions.Skip((page - 1) * Constants.Pagination.PageSize).Take(Constants.Pagination.PageSize).ToListAsync();
            return resultItems;
        }
    }
}
