using EducationApp.BusinessLogicLayer.Models.Authors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorModel> GetAllAsync();
        Task<AuthorItemModel> GetByIdAsync(string id);
        Task<AuthorItemModel> GetByNameASync(string name);
        Task AddAsync(AuthorItemModel entity);
        Task DeleteAsync(string id);
        Task EditAsync(AuthorItemModel entity);
    }
}
