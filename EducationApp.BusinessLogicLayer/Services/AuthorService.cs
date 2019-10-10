using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Models.Response;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interface;
using System.Threading.Tasks;
using AscendingDescending = EducationApp.BusinessLogicLayer.Models.Enums.Enums.AscendingDescending;
using AscDescConvert = EducationApp.DataAccessLayer.Entities.Enums.Enums.AscendingDescending;
using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.BusinessLogicLayer.Common.Constants;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
           _authorRepository = authorRepository;
        }

        public async Task<BaseModel> CreateAsync(string authorName)
        {
            var baseModel = new BaseModel();
            if (string.IsNullOrWhiteSpace(authorName))
            {
                baseModel.Errors.Add(Constants.Errors.NotFount);
                return baseModel;
            }
            var author = new Author()
            {
                Name = authorName
            };
            if(await _authorRepository.CreateAsync(author))
            {
                return baseModel;
            }
            baseModel.Errors.Add(Constants.Errors.ErrorToUpdate);
            return baseModel; 
        }

        public async Task<BaseModel> RemoveAsync(int id)
        {
            var baseModel = new BaseModel();
            if (_authorRepository.GetByIdAsync(id) == null)
            {
                baseModel.Errors.Add(Constants.Errors.NotFount);
                return baseModel;
            }
            if(await _authorRepository.RemoveAsync(id))
            {
                return baseModel;
            }
            baseModel.Errors.Add(Constants.Errors.ErrorToUpdate);
            return baseModel;
        }

        public async Task<BaseModel> EditAsync(AuthorModelItem editAuthorModelItem)
        {
            var baseModel = new BaseModel();
            var author = await _authorRepository.GetByIdAsync(editAuthorModelItem.Id);
            if (author == null)
            {
                baseModel.Errors.Add(Constants.Errors.NotFount);
                return baseModel;
            }
            author.Name = editAuthorModelItem.Name;
            if(await _authorRepository.EditAsync(author))
            {
                return baseModel;
            }
            baseModel.Errors.Add(Constants.Errors.ErrorToUpdate);
            return baseModel;
        }
        
        public async Task<AuthorModelItem> GetByIdAsync(int id)
        {
            var item= await _authorRepository.GetByIdAsync(id);
            var author = new AuthorModelItem()
            {
                Id = item.Id,
                Name = item.Name
            };
            return author;
        }

        public async Task<ResponseModel<AuthorWithProductsModelItem>> GetAllSortedAsync(int page, AscendingDescending sortById)
        {
            var authors = await _authorRepository.GetAllWithProductsAsync(page,(AscDescConvert)sortById);
            var result = new ResponseModel<AuthorWithProductsModelItem>();
            foreach (var author in authors.Items)
            {
                result.Items.Add(Mapper.MapToAuthor.MapToAuthorWithProductsModelItem(author));
            }
            result.TotalItems = authors.ItemsCount;
            return result;
        }  
    }
}
