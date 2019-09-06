using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using System.Linq;
using System.Threading.Tasks;

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

        public async void Create(TEntity item)
        {
            _dbSet.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAll()=> await _dbSet.ToListAsync();

        public async Task<TEntity> GetById(int id)=>await  _dbSet.FindAsync(id);

        public async void Delete(TEntity item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
