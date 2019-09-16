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


        public async Task AddAsync(AuthorItemModel entity)
        {
            var author = new Author() { Id = entity.id, Name = entity.Name };
            await _authorRepository.AddAsync(author);
        }

        public async Task DeleteAsync(string id)
        {
            await _authorRepository.DeleteAsync(id);
        }

        public async Task EditAsync(AuthorItemModel entity)
        {
            var author = new Author() { Id = entity.id, Name = entity.Name };
            await _authorRepository.EditAsync(author);
        }

        public async Task<AuthorModel> GetAllAsync()
        {
            List<Author> authors = await _authorRepository.GetAllAsync();
            var authorModel = new AuthorModel();
            foreach(var author in authors)
            {
                authorModel.Items.Add(new AuthorItemModel(author));
            }
            return authorModel;
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
