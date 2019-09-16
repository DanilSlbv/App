using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace EducationApp.DataAccessLayer.Repositories.Base
{
    public  class BaseEFRepository<TEntity> : IBaseEFRepository<TEntity> where TEntity:class
    {
        public ApplicationContext _context;
        public DbSet<TEntity> _dbSet;

        public BaseEFRepository(ApplicationContext _context)
        {
            this._context= _context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync()=> await _dbSet.ToListAsync();

        public async Task<TEntity> GetByIdAsync(string id)=>await  _dbSet.FindAsync(id);

        public async Task AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            TEntity entity = await GetByIdAsync(id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
