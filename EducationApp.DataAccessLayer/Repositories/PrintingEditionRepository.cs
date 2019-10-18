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
using EducationApp.DataAccessLayer.Models.PrintingEditions;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class PrintingEditionRepository : BaseEFRepository<PrintingEdition>, IPrintingEditionRepository
    {
        private readonly ApplicationContext _applicationContext;
        public PrintingEditionRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<long> GetIdByNameAsync(string name)
        {
            return await _applicationContext.PrintingEditions.Where(x => x.Name == name).Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<PrintingEditionWithAuthorsModel> GetWithAuthorsById(int id)
        {
            var printingEdition = await _applicationContext.AuthorInPrintingEditons.Include(x => x.Author).Include(y => y.PrintingEdition)
                .Where(x => x.PrintingEdition.Id == id).GroupBy(x => x.PrintingEditionId).Select(printing => new PrintingEditionWithAuthorsModel
                {
                    Id = printing.Key,
                    Name = printing.Select(x => x.PrintingEdition.Name).FirstOrDefault(),
                    Authors = printing.Select(x => x.Author).ToList(),
                    Currency = printing.Select(x => x.PrintingEdition.Currency).FirstOrDefault(),
                    Type = printing.Select(x => x.PrintingEdition.Type).FirstOrDefault(),
                    Price = printing.Select(x => x.PrintingEdition.Price).FirstOrDefault(),
                    Description = printing.Select(x => x.PrintingEdition.Description).FirstOrDefault()
                }).FirstOrDefaultAsync();
            return printingEdition;
        }

        public async Task<List<AuthorInPrintingEditons>> Sort()
        {
            var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=EducationStoreDb;Trusted_Connection=True;MultipleActiveResultSets=True";


            const string sql = @"SELECT * 
                                 From [EducationStoreDb].[dbo].[AuthorInPrintingEditons] AS AIPE
                                 INNER JOIN [EducationStoreDb].[dbo].[Authors] AS A ON AIPE.AuthorId=A.Id
                                 INNER JOIN [EducationStoreDb].[dbo].[PrintingEditions] AS PE ON AIPE.PrintingEditionId=PE.Id";
                                
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    var a = db.Query<AuthorInPrintingEditons>(sql).ToList();
                    return a;
                }
                catch (Exception ex)
                {
                    var e = ex;
                    return null;
                }
            }
        }

        public async Task<ResponseModel<PrintingEditionWithAuthorsModel>> SortWithAuthorsAsync(int page, PrintingEditionFilterModel filterModel)
        {
            Sort();
            var resultItems = new ResponseModel<PrintingEditionWithAuthorsModel>();
            IQueryable<AuthorInPrintingEditons> printingEditions = null;
            if (filterModel.SortByPrintingType == Type.None)
            {
                printingEditions = _applicationContext.AuthorInPrintingEditons.Include(x => x.Author).Where(x => x.Author.IsRemoved == false)
                    .Include(y => y.PrintingEdition).Where(x => x.PrintingEdition.IsRemoved == false).AsQueryable();
            }
            if (filterModel.SortByPrintingType != Type.None)
            {
                printingEditions = _applicationContext.AuthorInPrintingEditons.Include(x => x.Author).Include(y => y.PrintingEdition)
                    .Where(x => x.PrintingEdition.Type == filterModel.SortByPrintingType).AsQueryable();
            }
            if (!string.IsNullOrWhiteSpace(filterModel.SearchName))
            {
                printingEditions.Where(x => x.PrintingEdition.Name.Contains(filterModel.SearchName) || x.Author.Name.Contains(filterModel.SearchName));
            }
            if (filterModel.minPrice >= Constants.Price.MinPriceValue || filterModel.maxPrice <= Constants.Price.MaxPriceValue)
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
            var printings = await printingEditions.GroupBy(x => x.PrintingEditionId).Select(printingEdition => new PrintingEditionWithAuthorsModel
            {
                Id = printingEdition.Key,
                Name = printingEdition.Select(x => x.PrintingEdition.Name).FirstOrDefault(),
                Authors = printingEdition.Select(x => x.Author).ToList(),
                Currency = printingEdition.Select(x => x.PrintingEdition.Currency).FirstOrDefault(),
                Type = printingEdition.Select(x => x.PrintingEdition.Type).FirstOrDefault(),
                Price = printingEdition.Select(x => x.PrintingEdition.Price).FirstOrDefault(),
                Description = printingEdition.Select(x => x.PrintingEdition.Description).FirstOrDefault()
            }).Skip((page - 1) * Constants.Pagination.PageSize).Take(Constants.Pagination.PageSize).ToListAsync();
            resultItems.ItemsCount = printingEditions.Where(x => x.IsRemoved == false).Count();
            resultItems.Items = printings;
            return resultItems;
        }
    }
}
