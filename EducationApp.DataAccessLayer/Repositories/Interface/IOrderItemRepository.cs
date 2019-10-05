using EducationApp.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IOrderItemRepository:IBaseEFRepository<OrderItem>
    {
        Task<List<OrderItem>> GetAllAsync();
        Task RemoveAsync(int orderItemId);
        Task<List<OrderItem>> GetByOrderId(int orderId);
    }
}
