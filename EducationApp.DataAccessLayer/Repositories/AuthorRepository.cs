using EducationApp.DataAccessLayer.Common;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Models.Author;
using EducationApp.DataAccessLayer.Models.Pagination;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class AuthorRepository : BaseEFRepository<Author>, IAuthorRepository
    {
        private readonly ApplicationContext _applicationContext;
        public AuthorRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task RemoveAsync(int authorId)
        {
            var author = await _applicationContext.Authors.FindAsync(authorId);
            author.IsRemoved = true;
            _applicationContext.Authors.Update(author);
            await _applicationContext.SaveChangesAsync();
        }
        public async Task<List<Author>> GetAllAsync()
        {
            return await _applicationContext.Authors.Where(x => x.IsRemoved == false).ToListAsync();
        }

        public async Task<PaginationModel<AuthorWithProductsModel>> GetAllWithProductsAsync(int page)
        {
            var itemsCount = _applicationContext.Authors.Count();
            var authorsWithPrintingEditions = await (from author in _applicationContext.Authors
                                                     where author.IsRemoved==false
                                                     select new AuthorWithProductsModel()
                                                     {
                                                         AuthorId = author.Id,
                                                         AuthorName = author.Name,
                                                         Title = (from authorInPrintingsEdition in _applicationContext.AuthorInPrintingEditons
                                                                  join printing in _applicationContext.PrintingEditions on authorInPrintingsEdition.PrintingEditionId equals printing.Id
                                                                  where author.Id == authorInPrintingsEdition.AuthorId && printing.IsRemoved == false && authorInPrintingsEdition.IsRemoved==false
                                                                  select printing.Name
                                                                  ).ToList()
                                                     }).Skip((page - 1) * Constants.Pagination.PageSize).Take(Constants.Pagination.PageSize).ToListAsync();
            var allItems = new PaginationModel<AuthorWithProductsModel>()
            {
                Items = authorsWithPrintingEditions,
                ItemsCount=itemsCount
            };
            return allItems;
        }
    }
}

