using Dapper;
using EducationApp.DataAccessLayer.Common.Constants;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Models.Authors;
using EducationApp.DataAccessLayer.Models.Response;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AscendingDescending = EducationApp.DataAccessLayer.Entities.Enums.Enums.AscendingDescending;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class AuthorRepository : BaseEFRepository<Author>, IAuthorRepository
    {
        private string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=EducationStoreDb;Trusted_Connection=True;MultipleActiveResultSets=True";
        private readonly ApplicationContext _applicationContext;
        public AuthorRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            await GetAllWithProductsAsyncDapper();
            return await _applicationContext.Authors.Where(x => x.IsRemoved == false).ToListAsync();
        }

        public List<Author> GetAllAsyncDapper()
        {
            const string sql = @"SELECT *
                                 FROM [EducationStoreDb].[dbo].[Authors]  
                                 WHERE IsRemove=false";

            var authors = new List<Author>();
            using (var db = new SqlConnection(_connectionString))
            {
                try
                {
                    authors = db.Query<Author>(sql).ToList();
                }
                catch (Exception ex)
                {
                    var e = ex;
                }
            }
            return authors;
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
            if (sortById == AscendingDescending.Ascending)
            {
                authorsWithPrintingEditions = authorsWithPrintingEditions.OrderBy(x => x.AuthorId);
            }
            if (sortById == AscendingDescending.Descending)
            {
                authorsWithPrintingEditions = authorsWithPrintingEditions.OrderByDescending(x => x.AuthorId);
            }
            allItems.Items = await authorsWithPrintingEditions.Skip((page - 1) * Constants.Pagination.PageSize).Take(Constants.Pagination.PageSize).ToListAsync();
            allItems.ItemsCount = _applicationContext.Authors.Where(x => x.IsRemoved == false).Count();
            return allItems;
        }
        public async Task<AuthorWithProductsModel> GetAllWithProductsAsyncDapper()
        {
            string sql = $@"SELECT A.*,PE.*
                         From [EducationStoreDb].[dbo].[AuthorInPrintingEditons] AS AIPE
                         INNER JOIN [EducationStoreDb].[dbo].[Authors] AS A ON AIPE.AuthorId=A.Id
                         INNER JOIN [EducationStoreDb].[dbo].[PrintingEditions] AS PE ON AIPE.AuthorId=A.Id
                         WHERE A.Id=AIPE.AuthorId";
            using (var db = new SqlConnection(_connectionString))
            {
                try
                {
                    var resultItems = new Dictionary<long, AuthorWithProductsModel>();
                    await db.QueryAsync<Author, PrintingEdition, AuthorWithProductsModel>(sql,
                        (A, PE) =>
                        {
                            if (!resultItems.TryGetValue(A.Id, out AuthorWithProductsModel authorWithProductsModel))
                            {
                                authorWithProductsModel = new AuthorWithProductsModel();
                                authorWithProductsModel.AuthorId = A.Id;
                                authorWithProductsModel.AuthorName = A.Name;
                                authorWithProductsModel.PrintingEditions = new List<PrintingEdition>();
                                resultItems.Add(A.Id, authorWithProductsModel);
                            }
                            authorWithProductsModel.PrintingEditions.Add(PE);
                            return authorWithProductsModel;
                        });
                }catch(Exception ex)
                {
                    var e = ex;
                    return null;
                }
            }
            return null;
        }
    }
}

