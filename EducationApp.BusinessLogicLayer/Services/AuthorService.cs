using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<bool> EditAsync(EditAuthorModelItem editAuthorModelItem)
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

        public async Task<AuthorModel> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            var authorModel = new AuthorModel();
            foreach(var author in authors)
            {
                authorModel.Items.Add(new AuthorModelItem(author));
            }
            return authorModel;
        }

        public async Task<AuthorModelItem> GetByIdAsync(int id)
        {
            var item =new AuthorModelItem( await _authorRepository.GetByIdAsync(id));
            return item;
        }

        public async Task<AuthorModelItem> GetByNameASync(string name)
        {
            if (name == null)
            {
                return null;
            }
            var author = new AuthorModelItem(await _authorRepository.GetByNameAsync(name));
            return author;
        }

        public async Task<AuthorInPrintingEditionsModel> GetAuthorsWithPrintingEditions()
        {
            var authors = await _authorRepository.GetAllAsync();
            AuthorInPrintingEditionsModel authorPrintingEditionsModel = new AuthorInPrintingEditionsModel();
            foreach(var author in authors)
            {
                var authorInPrintingEditionItem = new AuthorInPrintingEditionsModelItem();
                authorInPrintingEditionItem.AuthorId = author.Id;
                authorInPrintingEditionItem.Name = author.Name;
                var printingEditionsId = await _authorInPrintingEditionRepository.GetPrintingEditionsByAuthorIdAsync(author.Id);
                foreach(var item in printingEditionsId)
                {
                    var printingEdition = await _printingEditionRepository.GetByIdAsync(item.PrintingEditionId);
                    authorInPrintingEditionItem.Products.Add(printingEdition.Name);
                }
                authorPrintingEditionsModel.Items.Add(authorInPrintingEditionItem);
            }
            return authorPrintingEditionsModel;
        }

    }
}
