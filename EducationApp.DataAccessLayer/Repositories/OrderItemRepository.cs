using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class OrderItemRepository: BaseEFRepository<OrderItem>, IOrderItemRepository
    {
        private readonly ApplicationContext _applicationContext;
        public OrderItemRepository(ApplicationContext applicationContext):base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<List<OrderItem>> GetAllAsync() => await _applicationContext.OrderItems.Where(x => x.IsRemoved == false).ToListAsync();

        public async Task<List<OrderItem>> GetByOrderId(int orderId)
        {
            return await _applicationContext.OrderItems.Where(x => x.OrderId == orderId && x.IsRemoved == false).ToListAsync();
        }

        public async Task RemoveAsync(int orderItemId)
        {
            var author = await _applicationContext.Authors.FindAsync(orderItemId);
            author.IsRemoved = true;
            _applicationContext.Authors.Update(author);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
