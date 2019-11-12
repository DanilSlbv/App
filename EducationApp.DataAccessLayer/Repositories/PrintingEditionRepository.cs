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
        private string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=EducationStoreDb;Trusted_Connection=True;MultipleActiveResultSets=True";
        private readonly ApplicationContext _applicationContext;
        public PrintingEditionRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<long> GetIdByNameAsync(string name)
        {
            return await _applicationContext.PrintingEditions.Where(x => x.Name == name).Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<long> GetIdByNameAsyncDapper(string name)
        {
            string sql = $"SELECT Id FROM [EducationStoreDb].[dbo].[PrintingEditions] WHERE Name={name}";
            using (var db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<long>(sql);
            }
        }

        public async Task<PrintingEditionWithAuthorsModel> GetWithAuthorsByIdAsync(long id)
        {
            var dapperResult = await GetWithAuthorsByIdAsyncDapper(id);
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

        public async Task<PrintingEditionWithAuthorsModel> GetWithAuthorsByIdAsyncDapper(long id)
        {
            var response = new PrintingEditionWithAuthorsModel();
            string sql = $@"SELECT *
                            FROM [EducationStoreDb].[dbo].[PrintingEditions]
                            WHERE Id={id};

                            Select AIPE.* , A.* 
                            From [EducationStoreDb].[dbo].[AuthorInPrintingEditons] as AIPE
                            Inner Join [EducationStoreDb].[dbo].[Authors] as A on AIPE.AuthorId=A.Id
                            Where AIPE.PrintingEditionId={id};";
            using (var db = new SqlConnection(_connectionString))
            {
                await db.OpenAsync();
                try
                {
                    using (var results = await db.QueryMultipleAsync(sql, null))
                    {
                        var printing = results.Read<PrintingEdition>().First();
                        var authors = results.Read<Author>().ToList();
                        response.Id = printing.Id;
                        response.Name = printing.Name;
                        response.Description = printing.Description;
                        response.Currency = printing.Currency;
                        response.Type = printing.Type;
                        response.Price = printing.Price;
                        response.Authors = authors;
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    var exception = ex;
                    return null;
                }
            }
        }

        public async Task<ResponseModel<PrintingEditionWithAuthorsModel>> SortWithAuthorsAsync(int page, PrintingEditionFilterModel filterModel)
        {
            //await SortWithAuthorsAsyncDapper(filterModel);
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

        public async Task<List<AuthorWithProductsModel>> GetAllWithProductsAsyncDapper(int page, int count)
        {
            string sql = $@"SELECT PE.*
                            FROM [EducationStoreDb].[dbo].[PrintingEditions];
            
                            SELECT A.*
                            FROM [EducationStoreDb].[dbo].[AuthorInPrintingEditons]
                            INNER JOIN [EducationStoreDb].[dbo].[Authors] AS aipe.AuthorId=@authorId
                            WHERE AuthorId=@authorId;";
            using (var db = new SqlConnection(_connectionString))
            {
                using (var multi = await db.QueryMultipleAsync(sql))
                {
                    var items = new List<AuthorWithProductsModel>();
                    await db.OpenAsync();
                    using (var results = await db.QueryMultipleAsync(sql))
                    {
                        var printings = await multi.ReadAsync<PrintingEdition>();
                        foreach (var item in printings)
                        {
                            var authors = await multi.ReadAsync<Author>();
                            items.Add(new AuthorWithProductsModel()
                            {

                            });
                        }
                    }
                    return items;
                }
            }
        }

        /* public async Task<List<PrintingEditionWithAuthorsModel>> SortWithAuthorsAsyncDapper(PrintingEditionFilterModel filterModel)
         {
             var result = new List<PrintingEditionWithAuthorsModel>();
             const string sql = @"
                                  Select PE.*,A.* 
                                  From [EducationStoreDb].[dbo].[AuthorInPrintingEditons] as AIPE
                                  Inner Join [EducationStoreDb].[dbo].[Authors] as A on AIPE.AuthorId=A.Id
                                  Inner Join [EducationStoreDb].[dbo].[PrintingEditions] as PE on AIPE.PrintingEdition.Id=PE.Id  
                                  Where PE.Id=AIPE.PrintingEditionId;";

             using (var db = new SqlConnection(_connectionString))
             {
                 try
                 {
                     await db.OpenAsync();

                     var results = db.Query<AuthorInPrintingEditons, Author, PrintingEdition, PrintingEditionWithAuthorsModel>(sql,
                         (authorInPrintingEditions, author, printingEdition) =>
                         {
                             var item = new PrintingEditionWithAuthorsModel();

                             if (item.Authors == null)
                             {
                                 item.Authors = new List<Author>();
                             }
                             result.Add(
                                 )
                         });

                 }
                 catch (Exception ex)
                 {
                     var e = ex;
                     return null;
                 }
             }
         }*/
    }
}
