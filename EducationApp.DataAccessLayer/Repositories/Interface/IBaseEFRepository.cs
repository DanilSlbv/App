using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    interface IBaseEFRepository<TEntity> where TEntity:class
    {
        void Create(TEntity item);
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        void DeleteByItem(TEntity item);
    }
}
