using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using EducationApp.DataAccessLayer.Models.Response;
using EducationApp.DataAccessLayer.Models.Filters;
using Type = EducationApp.DataAccessLayer.Entities.Enums.Enums.Type;
using AscendingDescending = EducationApp.DataAccessLayer.Entities.Enums.Enums.AscendingDescending;
using EducationApp.DataAccessLayer.Common.Constants;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class PrintingEditionRepository : BaseEFRepository<PrintingEdition>, IPrintingEditionRepository
    {
        private readonly ApplicationContext _applicationContext;
        public PrintingEditionRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<AuthorInPrintingEditons> GetWithAuthorsById(int id)
        {
            var printingEdition =await _applicationContext.AuthorInPrintingEditons.Include(x => x.Author).Include(y => y.PrintingEdition).
                Where(x=>x.PrintingEdition.Id==id).FirstOrDefaultAsync();
            return printingEdition;
        }

        public async Task<ResponseModel<AuthorInPrintingEditons>> SortWithAuthorsAsync(int page, PrintingEditionFilterModel filterModel)
        {
            var resultItems = new ResponseModel<AuthorInPrintingEditons>();
            IQueryable<AuthorInPrintingEditons> printingEditions=null;
            if (filterModel.SortByPrintingType== Type.None)
            {
                printingEditions = _applicationContext.AuthorInPrintingEditons.Include(x => x.Author).Include(y => y.PrintingEdition).AsQueryable();
              
            }
            if (filterModel.SortByPrintingType != Type.None)
            {
                printingEditions =  _applicationContext.AuthorInPrintingEditons.Include(x => x.Author).Include(y => y.PrintingEdition)
                    .Where(x=>x.PrintingEdition.Type== filterModel.SortByPrintingType).AsQueryable();

            }
            if (filterModel.minPrice >= Constants.Price.MinPriceValue || filterModel.maxPrice<=Constants.Price.MaxPriceValue)
            {
                printingEditions = printingEditions.Where(x => x.PrintingEdition.Price >= filterModel.minPrice && x.PrintingEdition.Price <= filterModel.maxPrice);
            }
            if (filterModel.SortByPrice == AscendingDescending.Ascending)
            {
                printingEditions = printingEditions.OrderBy(x => x.PrintingEdition.Price);
            }
            if (filterModel.SortByPrice == AscendingDescending.Descending)
            {
                printingEditions = printingEditions.OrderByDescending(x => x.PrintingEdition.Price);
            }
            resultItems.ItemsCount = printingEditions.Count();
            resultItems.Items =await printingEditions.Skip((page - 1) * Constants.Pagination.PageSize).Take(Constants.Pagination.PageSize).ToListAsync();
            return resultItems;
        }
    }
}
