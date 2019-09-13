using EducationApp.BusinessLogicLayer.Models.Authors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorItemModel>> GetAllAsync();
        Task<AuthorItemModel> GetByIdAsync(string id);
        Task<AuthorItemModel> GetByNameASync(string name);
        Task AddItemAsync(AuthorItemModel entity);
        Task DeleteItemAsync(string id);
        Task EditItemAsync(AuthorItemModel entity);
    }
}
