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
        Task<AuthorModelItem> GetByIdAsync(string id);
        Task<AuthorModelItem> GetByNameASync(string name);
        Task AddAsync(AuthorModelItem entity);
        Task DeleteAsync(string id);
        Task EditAsync(AuthorModelItem entity);
    }
}
