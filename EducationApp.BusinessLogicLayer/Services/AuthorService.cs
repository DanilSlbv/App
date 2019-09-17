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


        public async Task AddAsync(AuthorModelItem authorItemModel)
        {
            var author = new Author() { Id = authorItemModel.id, Name = authorItemModel.Name };
            await _authorRepository.AddAsync(author);
        }

        public async Task DeleteAsync(string id)
        {
            await _authorRepository.DeleteAsync(id);
        }

        public async Task EditAsync(AuthorModelItem authorItemModel)
        {
            var author = new Author() { Id = authorItemModel.id, Name = authorItemModel.Name };
            await _authorRepository.EditAsync(author);
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
