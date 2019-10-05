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
        Task<AuthorModelItem> GetByIdAsync(int id);
        Task<AuthorModelItem> GetByNameASync(string name);
        Task<bool> AddAsync(string authorName);
        Task<bool> RemoveAsync(int id);
        Task<bool> EditAsync(EditAuthorModelItem editAuthorModelItem);
        Task<AuthorInPrintingEditionsModel> GetAuthorsWithPrintingEditions();
    }
}
