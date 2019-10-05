using EducationApp.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IOrderRepository:IBaseEFRepository<Order>
    {
        Task<List<Order>> GetAllAsync();
        Task<List<Order>> GetOrdersByUserIdAsync(string UserId);
        Task AddOrder(Order order);
        Task RemoveAsync(int orderId);
    }
}
