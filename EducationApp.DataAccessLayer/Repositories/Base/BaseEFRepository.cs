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
        public readonly ApplicationContext _context;
        public readonly DbSet<TEntity> _dbSet;

        public BaseEFRepository(ApplicationContext context)
        {
            _context= context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)=>await  _dbSet.FindAsync(id);

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
    }
}
