using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IBaseEFRepository<TEntity> where TEntity:class
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(string id);
        Task DeleteAsync(string id);
    }
}
