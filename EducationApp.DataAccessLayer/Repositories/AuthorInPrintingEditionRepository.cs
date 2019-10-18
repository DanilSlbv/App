using EducationApp.DataAccessLayer.Repositories.Interface;
using System.Collections.Generic;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAcessLayer.AppContext;
using System.Threading.Tasks;
using EducationApp.DataAccessLayer.Repositories.Base;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EducationApp.DataAccessLayer.Models.Authors;
using System.Data.SqlClient;
using Dapper;
using System;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class AuthorInPrintingEditionRepository : BaseEFRepository<AuthorInPrintingEditons>, IAuthorInPrintingEditionRepository
    {
        private string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=EducationStoreDb;Trusted_Connection=True;MultipleActiveResultSets=True";
        private readonly ApplicationContext _applicationContext;
        public AuthorInPrintingEditionRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<List<long>> GetIdByPrintingEditionIdAsync(long printingEditionId)
        {
            return await _applicationContext.AuthorInPrintingEditons.Where(x => x.PrintingEditionId == printingEditionId).Select(x => x.Id).ToListAsync();
        }

        public List<long> GetIdByPrintingEditionIdAsyncDapper(long printingEditionId)
        {
            string sql = $@"SELECT Id FROM [EducationStoreDb].[dbo].[AuthorInPrintingEditons] WHERE PrintingEditionId={printingEditionId}";
            using (var db = new SqlConnection(_connectionString))
            {
                try
                {
                    var a = db.Query<long>(sql).ToList();
                    return a;
                }
                catch (Exception ex)
                {
                    var e = ex;
                    return null;
                }
            }
        }

        public async Task<List<AuthorInPrintingEditons>> GetAuthorsByPrintingEditionId(long printingEditionId)
        {
            return await _applicationContext.AuthorInPrintingEditons.Where(x => x.PrintingEditionId == printingEditionId).ToListAsync();
        }

        public List<Author> GetAuthorByPrintingEditionIdDapper(long printingEditionId)
        {
            string sql = $@"SELECT A.*
                         FROM [EducationStoreDb].[dbo].[AuthorInPrintingEditons] AS AIPE
                         INNER JOIN [EducationStoreDb].[dbo].[Authors] AS A ON AIPE.AuthorId=A.Id
                         WHERE AIPE.PrintingEditionId={printingEditionId}";
            using (var db = new SqlConnection(_connectionString))
            {
                try
                {
                    var a = db.Query<Author>(sql).ToList();
                    return a;
                }
                catch (Exception ex)
                {
                    var e = ex;
                    return null;
                }
            }
        }

        public async Task<List<AuthorInPrintingEditons>> GetPrintingEditionsByAuthorIdAsync(long authorId)
        {
            return await _applicationContext.AuthorInPrintingEditons.Where(x => x.AuthorId == authorId && x.IsRemoved == false).ToListAsync();
        }

        public List<PrintingEdition> GetPrintingEditionByAuthorIdAsyncDapper(long authorId)
        {
            string sql = $@"SELECT PE.*
                        FROM [EducationStoreDb].[dbo].[AuthorInPrintingEditons] AS AIPE
                        INNER JOIN [EducationStoreDb].[dbo].[PrintingEditions] AS PE ON AIPE.PrintingEditionId=PE.Id
                        WHERE AIPE.AuthorId={authorId}";
            using (var db = new SqlConnection(_connectionString))
            {
                try
                {
                    var a = db.Query<PrintingEdition>(sql).ToList();
                    return a;
                }
                catch (Exception ex)
                {
                    var e = ex;
                    return null;
                }
            }
        }

        public async Task<List<AuthorInPrintingEditons>> GetAllAsync()
        {
            return await _applicationContext.AuthorInPrintingEditons.Where(x => x.IsRemoved == false).ToListAsync();
        }

        public async Task<List<AuthorInPrintingEditons>> GetAllAsyncDapper()
        {
            string sql = $@"SELECT AIPE.*,PE.*,A.*
                        FROM [EducationStoreDb].[dbo].[AuthorInPrintingEditons] AS AIPE
                        INNER JOIN [EducationStoreDb].[dbo].[PrintingEditions] AS PE ON AIPE.PrintingEditionId=PE.Id
                        INNER JOIN [EducationStoreDb].[dbo].[PrintingEditions] AS A ON AIPE.AuthorId=A.Id";
            using (var db = new SqlConnection(_connectionString))
            {
                try
                {
                    var a = db.Query<AuthorInPrintingEditons, Author, PrintingEdition, AuthorInPrintingEditons>(sql,
                        (AIPE, A, PE) =>
                        {
                            AuthorInPrintingEditons authorInPrintingEditons;

                            authorInPrintingEditons = AIPE;
                            authorInPrintingEditons.PrintingEdition = PE;
                            authorInPrintingEditons.Author = A;
                            return authorInPrintingEditons;
                        },
                        splitOn:"Id").ToList();
                    return a;
                }
                catch (Exception ex)
                {
                    var e = ex;
                    return null;
                }
            }
        }

        public async Task<bool> ChekcIfAuthorExistAsync(long printingEditionId, long authorId)
        {
            var item = await _applicationContext.AuthorInPrintingEditons.Where(x => x.PrintingEditionId == printingEditionId && x.AuthorId == authorId).FirstOrDefaultAsync();
            if (item == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CheckIfAuthorExistAsyncDapper(long printignEditionId, long authorId)
        {
            string sql = "SELECT Id FROM [EducationStoreDb].[dbo].[AuthorInPrintingEditons] AS AIPE WHERE AIPE.AuthorId=@authorId AND AIPE.PrintingEditionId=@printignEditionId";
            using (var db = new SqlConnection(_connectionString))
            {
                try
                {
                    if (await db.QuerySingleAsync(sql, new { authorId, printignEditionId }) == null)
                    {
                        return false;
                    }
                    return true;

                }
                catch (Exception ex)
                {
                    var e = ex;
                    return false;
                }
            }
        }
    }
}
