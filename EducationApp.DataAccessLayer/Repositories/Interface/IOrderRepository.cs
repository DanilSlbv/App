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
        Task<List<Order>> GetUserOrdersAsync(string UserId);
        Task AddOrder(Order order);
    }
}
