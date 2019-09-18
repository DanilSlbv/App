using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class OrderItemRepository: IOrderItemRepository
    {
        private readonly ApplicationContext _applicationContext;

        public OrderItemRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task AddAsync(OrderItemRepository orderItemRepository)
        {
            await AddAsync(orderItemRepository);
        }

        public async Task DeleteAsync(string id)
        {
            await DeleteAsync(id);
        }

        public async Task EditAsync(OrderItemRepository orderItemRepository)
        {
            await EditAsync(orderItemRepository);
        }

        public async Task<List<OrderItemRepository>> GetAllAsync()
        {
            return await GetAllAsync();
        }

        public async Task<OrderItemRepository> GetByIdAsync(string id)
        {
            return await GetByIdAsync(id);
        }
    }
}
