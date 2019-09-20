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
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }


        public async Task AddAsync(AddAuthorModelItem addAuthorModelItem)
        {
            var author = new Author() { Name = addAuthorModelItem.Name };
            await _authorRepository.AddAsync(author);
        }

        public async Task DeleteAsync(string id)
        {
            await _authorRepository.DeleteAsync(id);
        }

        public async Task EditAsync(EditAuthorModelItem editAuthorModelItem)
        {
            var author = await _authorRepository.GetByIdAsync(editAuthorModelItem.id);
            if (author != null)
            {
                author.Name = editAuthorModelItem.Name;
                await _authorRepository.EditAsync(author);
            }
        }

        public async Task<AuthorModel> GetAllAsync()
        {
            List<Author> authors = await _authorRepository.GetAllAsync();
            var authorModel = new AuthorModel();
            foreach(var author in authors)
            {
                authorModel.Items.Add(new AuthorModelItem(author));
            }
            return authorModel;
        }

        public async Task<AuthorModelItem> GetByIdAsync(string id)
        {
            var item =new AuthorModelItem( await _authorRepository.GetByIdAsync(id));
            return item;
        }

        public async Task<AuthorModelItem> GetByNameASync(string name)
        {
            var author = new AuthorModelItem(await _authorRepository.GetByNameAsync(name));
            return author;
        }
    }
}
