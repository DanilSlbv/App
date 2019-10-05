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
    public class OrderRepository: BaseEFRepository<Order>, IOrderRepository
    {
        private readonly ApplicationContext _applicationContext;
        public OrderRepository(ApplicationContext applicationContext):base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<List<Order>> GetAllAsync() => await _applicationContext.Orders.Where(x => x.IsRemoved == false).ToListAsync();

        public async Task AddOrder(Order order)
        {
            _applicationContext.Orders.Add(order);
            await _applicationContext.SaveChangesAsync();
        }
        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _applicationContext.Orders.Where(x => x.UserId == userId && x.IsRemoved == false).ToListAsync();
        }        

        public async Task RemoveAsync(int orderId)
        {
            var order = await _applicationContext.Authors.FindAsync(orderId);
            order.IsRemoved = true;
            _applicationContext.Authors.Update(order);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
