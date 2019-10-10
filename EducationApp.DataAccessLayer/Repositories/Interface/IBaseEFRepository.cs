using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IBaseEFRepository<TEntity> where TEntity:class
    {
        Task<TEntity> GetByIdAsync(long id);
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> EditAsync(TEntity entity);
        Task<bool> RemoveAsync(long id);
    }
}
