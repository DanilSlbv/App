using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using System.Linq;
using System.Threading.Tasks;
using System;
using EducationApp.DataAccessLayer.Entities.Base;
using System.Data.SqlClient;
using Dapper;

namespace EducationApp.DataAccessLayer.Repositories.Base
{
    public class BaseEFRepository<TEntity> : IBaseEFRepository<TEntity> where TEntity : class
    {
        private string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=EducationStoreDb;Trusted_Connection=True;MultipleActiveResultSets=True";
        private string _tableName = nameof(TEntity);
        public readonly ApplicationContext _context;
        public readonly DbSet<TEntity> _dbSet;
        public readonly DbSet<BaseEntity> _dbEntity;

        public BaseEFRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _dbEntity = _context.Set<BaseEntity>();
        }

        public async Task<TEntity> GetByIdAsync(long id) => await _dbSet.FindAsync(id);

        public async Task<TEntity> GetByIdAsyncDapper(long id)
        {
            string sql = $@"SELECT * FROM [EducationStoreDb].[dbo].[{_tableName}] WHERE Id=id";
            using(var db=new SqlConnection(_connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<TEntity>(sql);
            }
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
        
        /*public async Task<bool> CreateAsync(TEntity entity)
        {
            string sql = $@"INSERT INTO [EducationStoreDb].[dbo].[{_tableName}](@entity) 
                            VALUES (@entity)";
            using(var db=new SqlConnection(_connectionString))
            {
                try
                {
                    if (await db.ExecuteAsync(sql, new { entity }) > 0)
                    {
                        return true;
                    }
                    return false;
                }catch(Exception ex)
                {
                    var e = ex;
                    return true;
                }

            }
        }*/

        public async Task<bool> EditAsync(TEntity entity)
        {
            _context.Update(entity);
            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

       /* public async Task<bool> EditAsync(TEntity entity)
        {
            string sql = $@"UPDATE [EducationStoreDb].[dbo].[{_tableName}] SET @entity";
            using (var db = new SqlConnection(_connectionString))
            {
                if(await db.ExecuteAsync(sql,new { entity })>0)
                {
                    return true;
                }
                return false;
            }
        }*/
    }
}
