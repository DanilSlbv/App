using EducationApp.DataAccessLayer.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAcessLayer.AppContext;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class AuthorInPrintingEditionRepository:IAuthorInPrintingEditionRepository
    {
        private readonly IBaseEFRepository<AuthorInPrintingEditons> _baseEFRepository;
        private readonly ApplicationContext _context;
        public AuthorInPrintingEditionRepository(IBaseEFRepository<AuthorInPrintingEditons> baseEFRepository, ApplicationContext context)
        {
            _baseEFRepository = baseEFRepository;
            _context = context;
        }

        public async Task AddItemAsync(AuthorInPrintingEditons entity)
        {
             await _baseEFRepository.AddItemAsync(entity);
        }

        public async Task DeleteItemAsync(string id)
        {
            await _baseEFRepository.DeleteItemAsync(id);
        }

        public async Task EditItemAsync(AuthorInPrintingEditons entity)
        {
            await _baseEFRepository.EditItemAsync(entity);
        }

        public async Task<List<AuthorInPrintingEditons>> GetAllAsync()
        {
            var items = await _baseEFRepository.GetAllAsync();
            return items;
        }

        public async Task<AuthorInPrintingEditons> GetByIdAsync(string id)
        {
            var item = await _baseEFRepository.GetByIdAsync(id);
            return item;
        }
    }
}
