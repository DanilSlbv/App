using EducationApp.DataAccessLayer.Common.Constants;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Models.Author;
using EducationApp.DataAccessLayer.Models.Response;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AscendingDescending = EducationApp.DataAccessLayer.Entities.Enums.Enums.AscendingDescending;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class AuthorRepository : BaseEFRepository<Author>, IAuthorRepository
    {
        private readonly ApplicationContext _applicationContext;
        public AuthorRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _applicationContext.Authors.Where(x => x.IsRemoved == false).ToListAsync();
        }

        public async Task<ResponseModel<AuthorWithProductsModel>> GetAllWithProductsAsync(int page, AscendingDescending sortById)
        {
            var allItems = new ResponseModel<AuthorWithProductsModel>();
            var authorsWithPrintingEditions = from author in _applicationContext.Authors
                                              where author.IsRemoved == false
                                              select new AuthorWithProductsModel()
                                              {
                                                  AuthorId = author.Id,
                                                  AuthorName = author.Name,
                                                  PrintingEditions = (from authorInPrintingsEdition in _applicationContext.AuthorInPrintingEditons
                                                           join printing in _applicationContext.PrintingEditions on authorInPrintingsEdition.PrintingEditionId equals printing.Id
                                                           where author.Id == authorInPrintingsEdition.AuthorId && printing.IsRemoved == false && authorInPrintingsEdition.IsRemoved == false
                                                           select printing
                                                           ).ToList()
                                              };
            if (sortById==AscendingDescending.Ascending)
            {
                authorsWithPrintingEditions = authorsWithPrintingEditions.OrderBy(x => x.AuthorId);
            }
            if (sortById == AscendingDescending.Descending)
            {
                authorsWithPrintingEditions = authorsWithPrintingEditions.OrderByDescending(x => x.AuthorId);
            }
            allItems.Items=await authorsWithPrintingEditions.Skip((page - 1) * Constants.Pagination.PageSize).Take(Constants.Pagination.PageSize).ToListAsync();
            allItems.ItemsCount = _applicationContext.Authors.Count();   
            return allItems;
        }
    }
}

