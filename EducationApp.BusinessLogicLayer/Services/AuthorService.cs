using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;
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


        public async Task AddItemAsync(AuthorItemModel entity)
        {
            var author = new Author() { Id = entity.id, Name = entity.Name };
            await _authorRepository.AddItemAsync(author);
        }

        public async Task DeleteItemAsync(string id)
        {
            await _authorRepository.DeleteItemAsync(id);
        }

        public async Task EditItemAsync(AuthorItemModel entity)
        {
            var author = new Author() { Id = entity.id, Name = entity.Name };
            await _authorRepository.EditItemAsync(author);
        }

        public async Task<List<AuthorItemModel>> GetAllAsync()
        {
            List<Author> authors = await _authorRepository.GetAllAsync();
            var authorModel = new AuthorModel();
            foreach(var i in authors)
            {
                authorModel.Items.Add(new AuthorItemModel(i));
            }
            return authorModel.Items;
        }

        public async Task<AuthorItemModel> GetByIdAsync(string id)
        {
            var item =new AuthorItemModel( await _authorRepository.GetByIdAsync(id));
            return item;
        }

        public async Task<AuthorItemModel> GetByNameASync(string name)
        {
            var item = new AuthorItemModel(await _authorRepository.GetByNameAsync(name));
            return item;
        }
    }
}
