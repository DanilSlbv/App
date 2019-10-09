using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Models.Pagination;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AscendingDescending = EducationApp.BusinessLogicLayer.Models.Enums.Enums.AscendingDescending;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class AuthorService : IAuthorService
    {

        private readonly IAuthorRepository _authorRepository;
        private readonly IAuthorInPrintingEditionRepository _authorInPrintingEditionRepository;
        private readonly IPrintingEditionRepository _printingEditionRepository;

        public AuthorService(IAuthorRepository authorRepository,IAuthorInPrintingEditionRepository authorInPrintingEditionRepository,IPrintingEditionRepository printingEditionRepository)
        {
            _authorInPrintingEditionRepository = authorInPrintingEditionRepository;
             _authorRepository = authorRepository;
            _printingEditionRepository = printingEditionRepository;
        }

 

        public async Task<bool> AddAsync(string authorName)
        {
            if (authorName== null)
            {
                return false;
            }
            var author = new Author()
            {
                Name = authorName
            };
            await _authorRepository.AddAsync(author);
            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            if (_authorRepository.GetByIdAsync(id) == null)
            {
                return false;
            }
            await _authorRepository.RemoveAsync(id);
            return true;
        }

        public async Task<bool> EditAsync(AuthorModelItem editAuthorModelItem)
        {
            var author = await _authorRepository.GetByIdAsync(editAuthorModelItem.Id);
            if (author != null)
            {
                author.Name = editAuthorModelItem.Name;
                await _authorRepository.EditAsync(author);
                return true;
            }
            return false;
        }
        
        public async Task<AuthorModelItem> GetByIdAsync(int id)
        {
            var item= await _authorRepository.GetByIdAsync(id);
            var author = new AuthorModelItem() { Id = item.Id, Name = item.Name };
            return author;
        }

        public async Task<PaginationModel<AuthorWithProductsModelItem>> GetAllSortedAsync(int page, AscendingDescending sortById)
        {
            var result = new PaginationModel<AuthorWithProductsModelItem>();
            var authors = await _authorRepository.GetAllWithProductsAsync(page);
            foreach (var author in authors.Items)
            {
                result.Items.Add(new AuthorWithProductsModelItem(author));
            }
            if (sortById==AscendingDescending.Ascending)
            {
                result.Items.OrderBy(x => x.AuthorId);
            }
            if (sortById==AscendingDescending.Descending)
            {
                result.Items.OrderByDescending(x => x.AuthorId);
            }
            result.TotalItems = authors.ItemsCount;
            return result;
        }  
    }
}
