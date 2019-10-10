using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using System.Linq;
using System.Threading.Tasks;
using System;
using EducationApp.DataAccessLayer.Entities.Base;

namespace EducationApp.DataAccessLayer.Repositories.Base
{
    public class BaseEFRepository<TEntity> : IBaseEFRepository<TEntity> where TEntity : class
    {
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

        public async Task<bool> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> EditAsync(TEntity entity)
        {
            _context.Update(entity);
            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> RemoveAsync(long id)
        {
            var item = await _dbEntity.FindAsync(id);
            item.IsRemoved = true;
            _dbEntity.Update(item);
            if( await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
